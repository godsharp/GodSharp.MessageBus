using GodSharp.Bus.Messages.Transfers;

namespace GodSharp.Bus.Messages.Serialization
{
    public interface ISerializationAdapter : System.IDisposable
    {
        byte[] PacketSerialize(Packet packet, ISerializer serializer);
        Packet PacketDeserialize(byte[] buffer, ISerializer serializer);
        byte[] MessagePackSerialize(MessagePack pack, ISerializer serializer);
        MessagePack MessagePackDeserialize(byte[] buffer, ISerializer serializer);
        byte[] MessagePackSerialize<T>(MessagePack<T> pack, ISerializer serializer);
        MessagePack<T> MessagePackDeserialize<T>(byte[] buffer, ISerializer serializer);
    }
}
