using GodSharp.Bus.Messages.Tests._Messages;

namespace GodSharp.Bus.Messages.Serialization
{
    public class SerializationAdapter : SerializationAdapter<MyPacket,MyMessagePack>
    {
        public override byte[] MessagePackSerialize<T>(MessagePack<T> pack, ISerializer serializer) => serializer.Serialize(pack as MyMessagePack<T>);

        public override MessagePack<T> MessagePackDeserialize<T>(byte[] buffer, ISerializer serializer) => serializer.Deserialize<MyMessagePack<T>>(buffer);
    }
}
