using System;

namespace GodSharp.Bus.Messages.Transfers
{
    public class Packet
    {
        public virtual string PackType { get; set; }
        public virtual byte[] Payload { get; set; }
        public virtual PacketType PacketType { get; set; }
        public virtual string FromId { get; set; }

        public Packet()
        {
        }

        public Packet(string packType, byte[] payload, PacketType packetType, string fromId)
        {
            PackType = packType ?? throw new ArgumentNullException(nameof(packType));
            Payload = payload;// ?? throw new ArgumentNullException(nameof(payload));
            PacketType = packetType;
            FromId = fromId ?? throw new ArgumentNullException(nameof(fromId));
        }
    }
}
