using GodSharp.Bus.Messages.Abstractions;
using System.Collections.Generic;

namespace GodSharp.Bus.Messages.Transfers
{
    public abstract class Transmitter : ITransmitter
    {
        private List<byte> list = new List<byte>();

        public Transmitter()
        {
        }

        public virtual string Id { get; protected set; }

        public virtual void OnReceived(byte[] buffer)
        {
            list.AddRange(buffer);

            byte[][] bytes = RemoteConfiguration.PacketCoder.Decode(list.ToArray(), out int position);
            if (position > 0 && list.Count>=position) list.RemoveRange(0, position);

            if(bytes?.Length>0)
            {
                foreach (var item in bytes)
                {
                    var packet = RemoteConfiguration.Adapter.PacketDeserialize(item, RemoteConfiguration.Serializer);

                    if (string.Equals(packet.FromId, Id, System.StringComparison.CurrentCultureIgnoreCase)) return;

                    switch (packet.PacketType)
                    {
                        case PacketType.Join:
                            (MessageBus.Instance as IMessageBusHanlder)?.Join(packet);
                            break;
                        case PacketType.Exit:
                            (MessageBus.Instance as IMessageBusHanlder)?.Exit(packet);
                            break;
                        default:
                            PacketHandler.Handle(packet, this);
                            break;
                    }
                }
            }
        }

        public void Handle<T>(Packet packet)
        {
            //if (string.Equals(packet.FromId, Id, System.StringComparison.CurrentCultureIgnoreCase)) return;

            (MessageBus.Instance as IMessageBusHanlder)?.Handle<T>(packet);
        }

        public void Send(Packet packet) => Send(RemoteConfiguration.PacketCoder.Encode(RemoteConfiguration.Adapter.PacketSerialize(packet, RemoteConfiguration.Serializer)));

        public abstract void Send(byte[] buffer);

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
        // ~Transmitter()
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

        public abstract void Start();
        public abstract void Stop();
        #endregion
    }
}