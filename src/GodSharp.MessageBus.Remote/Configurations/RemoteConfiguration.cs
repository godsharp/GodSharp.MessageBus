using GodSharp.Bus.Messages.Serialization;
using GodSharp.Bus.Messages.Transfers;

namespace GodSharp.Bus.Messages
{
    public class RemoteConfiguration
    {
        public static ISerializer Serializer { get; internal set; }
        public static ITransmitter Transmitter { get; internal set; }
        public static ISerializationAdapter Adapter { get; internal set; }
        public static IPacketCoder PacketCoder { get; internal set; }
    }
}