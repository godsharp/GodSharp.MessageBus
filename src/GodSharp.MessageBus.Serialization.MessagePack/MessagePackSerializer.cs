using Mp = MessagePack;

namespace GodSharp.Bus.Messages.Serialization.MessagePack
{
    public class MessagePackSerializer : ISerializer
    {
        public byte[] Serialize<T>(T t) => Mp.MessagePackSerializer.Serialize(t);

        public T Deserialize<T>(byte[] buffer) => Mp.MessagePackSerializer.Deserialize<T>(buffer);

        public void Dispose() { }
    }
}