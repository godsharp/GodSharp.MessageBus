using GodSharp.Bus.Messages.Transfers;

namespace GodSharp.Bus.Messages
{
    public static partial class Extensions
    {
        public static T MapTo<T>(this Packet packet) where T : Packet, new() => new T() { PackType = packet.PackType, Payload = packet.Payload, PacketType = packet.PacketType, FromId = packet.FromId };

        public static T MapTo<T>(this MessagePack pack) where T : MessagePack, new() => new T() { Name = pack.Name, DateTime = pack.DateTime };

        public static T MapTo<T, T2>(this MessagePack<T2> pack) where T : MessagePack<T2>, new() => new T() { Name = pack.Name, DateTime = pack.DateTime, Message = pack.Message };
    }
}
