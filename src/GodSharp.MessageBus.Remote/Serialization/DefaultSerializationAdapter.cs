using GodSharp.Bus.Messages.Transfers;

namespace GodSharp.Bus.Messages.Serialization
{
    public class DefaultSerializationAdapter : SerializationAdapter<Packet, MessagePack>
    {
        public override byte[] MessagePackSerialize<T>(MessagePack<T> pack, ISerializer serializer) => base.MessagePackSerialize<MessagePack<T>, T>(pack, serializer);

        public override MessagePack<T> MessagePackDeserialize<T>(byte[] buffer, ISerializer serializer) => base.MessagePackDeserialize<MessagePack<T>,T>(buffer,serializer);
    }
}
