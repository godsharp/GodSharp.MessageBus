namespace GodSharp.Bus.Messages.Transfers
{
    internal class PacketHandler<T>: IPacketHandler
    {
        public void Invoke(Packet packet, ITransmitter transmitter) => transmitter.Handle<T>(packet);
    }
}