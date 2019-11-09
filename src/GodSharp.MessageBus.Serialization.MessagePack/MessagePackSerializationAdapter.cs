namespace GodSharp.Bus.Messages.Serialization.MessagePack
{
    public class MessagePackSerializationAdapter : SerializationAdapter<MessagePackPacket, MessagePackMessagePack>
    {
        public override byte[] MessagePackSerialize<T>(MessagePack<T> pack, ISerializer serializer) => base.MessagePackSerialize<MessagePackMessagePack<T>, T>(pack, serializer);

        public override MessagePack<T> MessagePackDeserialize<T>(byte[] buffer, ISerializer serializer) => base.MessagePackDeserialize<MessagePackMessagePack<T>, T>(buffer, serializer);
    } 
}
