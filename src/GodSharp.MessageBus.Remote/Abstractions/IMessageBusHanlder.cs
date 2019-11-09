using GodSharp.Bus.Messages.Transfers;

namespace GodSharp.Bus.Messages.Abstractions
{
    internal interface IMessageBusHanlder
    {
        void Handle<T>(Packet packet);
        void Join(Packet packet);
        void Exit(Packet packet);
    }
}
