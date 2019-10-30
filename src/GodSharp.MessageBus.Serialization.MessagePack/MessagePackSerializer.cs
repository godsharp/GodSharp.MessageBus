#if NET40
using System;

namespace GodSharp.Bus.Messages.Serialization
{
    public class MessagePackSerializer : ISerializer
    {
        public byte[] Serialize<T>(T t) => throw new NotImplementedException();

        public T Deserialize<T>(byte[] buffer) => throw new NotImplementedException();

        public void Dispose() { }
    }
}
#else
using System;
using Mp = MessagePack;

namespace GodSharp.Bus.Messages.Serialization
{
    public class MessagePackSerializer : ISerializer
    {
        public byte[] Serialize<T>(T t) => Mp.MessagePackSerializer.Serialize(t);

        public T Deserialize<T>(byte[] buffer) => Mp.MessagePackSerializer.Deserialize<T>(buffer);

        public void Dispose() { }
    }
}
#endif