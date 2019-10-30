using GodSharp.Bus.Messages.Transfers;
using ProtoBuf;

namespace GodSharp.Bus.Messages.Tests._Messages
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public class MyPacket: Packet
    {
        public MyPacket() { }

        //public MyPacket(MessagePack<T> messagePack, PacketTypes type) : base(messagePack, type) { }
    }
}
