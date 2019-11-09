using MsgPack.Serialization;
using System.IO;

namespace GodSharp.Bus.Messages.Serialization.MsgPack
{
    public class MsgPackSerializer : ISerializer
    {
        public byte[] Serialize<T>(T t)
        {
            byte[] array = null;

            using (MemoryStream stream = new MemoryStream())
            {
                // Creates serializer.
                var serializer = MessagePackSerializer.Get<T>();
                // Pack obj to stream.
                serializer.Pack(stream, t);

                array = new byte[stream.Length];
                stream.Position = 0L;
                stream.Read(array, 0, array.Length);
            }

            return array;
        }

        public T Deserialize<T>(byte[] buffer)
        {
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                stream.Position = 0L;
                // Creates serializer.
                var serializer = MessagePackSerializer.Get<T>();
                // Unpack from stream.
                return serializer.Unpack(stream);
            }
        }

        public void Dispose()
        {
        }
    }
}
