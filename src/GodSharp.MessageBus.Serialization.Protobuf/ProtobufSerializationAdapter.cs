namespace GodSharp.Bus.Messages.Serialization.Protobuf
{
    public class ProtobufSerializationAdapter : SerializationAdapter<ProtobufPacket,ProtobufMessagePack>
    {
        public override byte[] MessagePackSerialize<T>(MessagePack<T> pack, ISerializer serializer) => base.MessagePackSerialize<ProtobufMessagePack<T>, T>(pack, serializer);

        public override MessagePack<T> MessagePackDeserialize<T>(byte[] buffer, ISerializer serializer) => base.MessagePackDeserialize<ProtobufMessagePack<T>,T>(buffer,serializer);
    }
}
