using GodSharp.Bus.Messages.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace GodSharp.Bus.Messages.Transfers
{
    public class PacketHandler
    {
        static ConcurrentDictionary<string, IPacketHandler> handlers = null;

        static PacketHandler()
        {
            handlers = new ConcurrentDictionary<string, IPacketHandler>();
        }

        public static void Register<T>() where T : IMessage
        {
            Type type = typeof(T);
            if (!typeof(IMessage).IsAssignableFrom(type)) return;

            handlers.TryAdd(type.FullName, new PacketHandler<T>());
        }

        public static void Registers(params Type[] types)
        {
            if (types.Length < 1) return;

            Type _t = typeof(IMessage);
            Type[] _types = types.Where(x => _t.IsAssignableFrom(x))?.ToArray();
            if (_types.Length < 1) return;

            Type _type = typeof(PacketHandler<>);
            foreach (var type in _types)
            {
                if (handlers.ContainsKey(type.FullName)) continue;
                Type t = _type.MakeGenericType(type);
                handlers.TryAdd(type.FullName, Activator.CreateInstance(t) as IPacketHandler);
            }
        }

        public static void Registers(params Assembly[] assemblies) => Registers(assemblies?.Select(x => x.GetTypes())?.SelectMany(x => x)?.ToArray());

        public static void Handle(Packet packet, ITransmitter transmitter)
        {
            if (handlers.TryGetValue(packet.PackType, out IPacketHandler handler))
            {
                if (handler == null) throw new NullReferenceException($"The type of {packet.PackType} not registed.");

                handler.Invoke(packet, transmitter);
            }
            else
            {
            }
        }
    }
}
