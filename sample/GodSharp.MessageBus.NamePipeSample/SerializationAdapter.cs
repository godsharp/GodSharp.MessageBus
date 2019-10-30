
using GodSharp.Bus.Messages;
using GodSharp.Bus.Messages.Serialization;

namespace GodSharp.MessageBus.NamePipeSample
{
    public class SerializationAdapter : SerializationAdapter<MyPacket,MyMessagePack>
    {
        public override byte[] MessagePackSerialize<T>(MessagePack<T> pack, ISerializer serializer) => base.MessagePackSerialize<MyMessagePack<T>, T>(pack, serializer);

        public override MessagePack<T> MessagePackDeserialize<T>(byte[] buffer, ISerializer serializer) => base.MessagePackDeserialize<MyMessagePack<T>,T>(buffer,serializer);
    }
}
