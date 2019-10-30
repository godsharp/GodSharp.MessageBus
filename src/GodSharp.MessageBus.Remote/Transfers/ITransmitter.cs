using System;

namespace GodSharp.Bus.Messages.Transfers
{
    public interface ITransmitter : IDisposable
    {
        string Id { get; }

        void OnReceived(byte[] buffer);
        void Handle<T>(Packet packet);
        void Send(Packet packet);
        void Send(byte[] buffer);
        void Start();
        void Stop();
    }
}
