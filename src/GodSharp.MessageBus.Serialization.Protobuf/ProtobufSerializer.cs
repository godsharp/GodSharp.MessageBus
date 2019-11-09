using ProtoBuf;
using System.IO;

namespace GodSharp.Bus.Messages.Serialization.Protobuf
{
    public class ProtobufSerializer : ISerializer
    {
        public byte[] Serialize<T>(T t)
        {
            byte[] array = null;

            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, t);

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
                return Serializer.Deserialize<T>(stream);
            }
        }

        public void Dispose()
        {
        }
    }
}
