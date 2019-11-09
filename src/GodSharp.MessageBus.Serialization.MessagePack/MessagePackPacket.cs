using GodSharp.Bus.Messages.Transfers;
using Mp = MessagePack; 

namespace GodSharp.Bus.Messages.Serialization.MessagePack
{
    [Mp.MessagePackObject]
    public class MessagePackPacket : Packet
    {
        [Mp.Key(1)]
        public override string PackType { get => base.PackType; set => base.PackType = value; }
        [Mp.Key(2)]
        public override byte[] Payload { get => base.Payload; set => base.Payload = value; }
        [Mp.Key(3)]
        public override PacketType PacketType { get => base.PacketType; set => base.PacketType = value; }
        [Mp.Key(4)]
        public override string FromId { get => base.FromId; set => base.FromId = value; }

        public MessagePackPacket()
        {
        }

        public MessagePackPacket(string packType, byte[] payload, PacketType packetType, string fromId) : base(packType, payload, packetType, fromId) { }

        public MessagePackPacket(Packet packet) : base(packet) { }
    }
}
