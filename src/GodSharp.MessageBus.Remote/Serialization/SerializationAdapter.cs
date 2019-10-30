using GodSharp.Bus.Messages.Transfers;

namespace GodSharp.Bus.Messages.Serialization
{
    public abstract class SerializationAdapter<T1, T2> : ISerializationAdapter
        where T1 : Packet,new()
        where T2 : MessagePack, new()
    {
        public byte[] PacketSerialize(Packet packet, ISerializer serializer) => serializer.Serialize(packet.MapTo<T1>());

        public Packet PacketDeserialize(byte[] buffer, ISerializer serializer) => serializer.Deserialize<T1>(buffer);

        public byte[] MessagePackSerialize(MessagePack pack, ISerializer serializer) => serializer.Serialize(pack.MapTo<T2>());

        public MessagePack MessagePackDeserialize(byte[] buffer, ISerializer serializer) => serializer.Deserialize<T2>(buffer);

        protected byte[] MessagePackSerialize<T3, T4>(MessagePack<T4> pack, ISerializer serializer)
            where T3 : MessagePack<T4>, new() 
            => serializer.Serialize(pack.MapTo<T3, T4>());

        protected MessagePack<T6> MessagePackDeserialize<T5, T6>(byte[] buffer, ISerializer serializer)
            where T5 : MessagePack<T6>, new()
            => serializer.Deserialize<T5>(buffer);

        public abstract byte[] MessagePackSerialize<T4>(MessagePack<T4> pack, ISerializer serializer);
        public abstract MessagePack<T4> MessagePackDeserialize<T4>(byte[] buffer, ISerializer serializer);

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SerializationAdapter()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
