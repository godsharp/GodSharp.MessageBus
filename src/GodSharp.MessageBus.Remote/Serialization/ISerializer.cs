using System;

namespace GodSharp.Bus.Messages.Serialization
{
    public interface ISerializer : IDisposable
    {
        byte[] Serialize<T>(T t);
        T Deserialize<T>(byte[] buffer);
    }
}
