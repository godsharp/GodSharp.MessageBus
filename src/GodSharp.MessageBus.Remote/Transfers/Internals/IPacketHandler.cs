namespace GodSharp.Bus.Messages.Transfers
{
    internal interface IPacketHandler
    {
        void Invoke(Packet packet, ITransmitter transmitter);
    }
}