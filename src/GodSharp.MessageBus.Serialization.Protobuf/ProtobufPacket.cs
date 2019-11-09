using GodSharp.Bus.Messages.Transfers;
using ProtoBuf;

namespace GodSharp.Bus.Messages.Serialization.Protobuf
{
    [ProtoContract]
    public class ProtobufPacket: Packet
    {
        [ProtoMember(1)]
        public override string PackType { get => base.PackType; set => base.PackType = value; }
        [ProtoMember(2)]
        public override byte[] Payload { get => base.Payload; set => base.Payload = value; }
        [ProtoMember(3)]
        public override PacketType PacketType { get => base.PacketType; set => base.PacketType = value; }
        [ProtoMember(4)]
        public override string FromId { get => base.FromId; set => base.FromId = value; }

        public ProtobufPacket()
        {
        }

        public ProtobufPacket(string packType, byte[] payload, PacketType packetType, string fromId) : base(packType, payload, packetType, fromId) { }

        public ProtobufPacket(Packet packet) : base(packet) { }
    }
}
