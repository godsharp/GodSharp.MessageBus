using GodSharp.Bus.Messages.Abstractions;
using GodSharp.Bus.Messages.Serialization;
using GodSharp.Bus.Messages.Transfers;
using System;
using System.Linq;
using System.Reflection;

namespace GodSharp.Bus.Messages
{
    public static partial class Extensions
    {
        public static IMessageBusBuilder AddRemote(this IMessageBusBuilder builder)
        {
            MessageBus.Instance = new MessageBusRemote();
            return builder.AddCoder();
        }

        public static IMessageBusBuilder AddSerializationAdapter<T>(this IMessageBusBuilder builder) where T : ISerializationAdapter => builder.AddSerializationAdapter(Activator.CreateInstance<T>());

        public static IMessageBusBuilder AddSerializationAdapter(this IMessageBusBuilder builder, ISerializationAdapter serializationAdapter)
        {
            RemoteConfiguration.Adapter = serializationAdapter ?? throw new ArgumentNullException(nameof(serializationAdapter));
            return builder;
        }

        public static IMessageBusBuilder AddSerializer<T>(this IMessageBusBuilder builder) where T : ISerializer => builder.AddSerializer(Activator.CreateInstance<T>());

        public static IMessageBusBuilder AddSerializer(this IMessageBusBuilder builder, ISerializer serializer)
        {
            RemoteConfiguration.Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            return builder;
        }

        public static IMessageBusBuilder AddTransmitter<T>(this IMessageBusBuilder builder) where T : Transmitter, ITransmitter => builder.AddTransmitter(Activator.CreateInstance<T>());
        public static IMessageBusBuilder AddTransmitter(this IMessageBusBuilder builder, ITransmitter transmitter)
        {
            RemoteConfiguration.Transmitter = transmitter ?? throw new ArgumentNullException(nameof(transmitter));
            return builder;
        }


        public static IMessageBusBuilder AddCoder(this IMessageBusBuilder builder) => builder.AddCoder<PacketCoder>();
        public static IMessageBusBuilder AddCoder<T>(this IMessageBusBuilder builder) where T : IPacketCoder => builder.AddCoder(Activator.CreateInstance<T>());
        public static IMessageBusBuilder AddCoder(this IMessageBusBuilder builder, IPacketCoder coder)
        {
            RemoteConfiguration.PacketCoder = coder ?? throw new ArgumentNullException(nameof(coder));
            return builder;
        }

        public static IMessageBusBuilder AddMessageType<T>(this IMessageBusBuilder builder) where T : IMessage
        {
            PacketHandler.Register<T>();
            return builder;
        }
        public static IMessageBusBuilder AddMessageTypes(this IMessageBusBuilder builder, params Type[] types)
        {
            PacketHandler.Registers(types);
            return builder;
        }
        public static IMessageBusBuilder AddMessageTypes(this IMessageBusBuilder builder, params Assembly[] assemblies)
        {
            PacketHandler.Registers(assemblies);
            return builder;
        }
        public static IMessageBusBuilder AddMessageTypes(this IMessageBusBuilder builder)
        {
            PacketHandler.Registers(Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(x => Assembly.Load(x)).ToArray());
            return builder;
        }

        public static IMessageBusOperator StartTransmitter(this IMessageBusOperator @operator)
        {
            RemoteConfiguration.Transmitter?.Start();
            return @operator;
        }
        public static IMessageBusOperator StopTransmitter(this IMessageBusOperator @operator)
        {
            RemoteConfiguration.Transmitter?.Stop();
            return @operator;
        }
    }
}
