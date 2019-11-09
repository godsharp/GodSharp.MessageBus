#if !NET40
using GodSharp.Bus.Messages.Serialization.MessagePack;
#endif
using GodSharp.Bus.Messages.Tests._Messages;
using GodSharp.Bus.Messages.Transfers;
using System;
using Xunit;

namespace GodSharp.Bus.Messages.Tests.Serialization
{
    public partial class SerializationTest
    {
        [Fact]
        public void MessagePack_Serialize_Deserialize_Test()
        {
#if !NET40
            using (MessagePackSerializer serializer = new MessagePackSerializer())
            {
                MpPerson person = new MpPerson() { Id = 2, Name = "Jerry", Address = new MpAddress() };

                byte[] buffer = serializer.Serialize(person);

                MpPerson _person = serializer.Deserialize<MpPerson>(buffer);

                Assert.Equal(person.Id, _person.Id);
                Assert.Equal(person.Name, _person.Name);

                MessagePackMessagePack<MpPerson> pack = new MessagePackMessagePack<MpPerson>(person);

                buffer = serializer.Serialize(pack);
                _person = serializer.Deserialize<MessagePackMessagePack<MpPerson>>(buffer)?.Message;

                Assert.Equal(person.Id, _person.Id);
                Assert.Equal(person.Name, _person.Name);

                Packet packet = new Packet(typeof(MpPerson).FullName, buffer, PacketType.Join, Guid.NewGuid().ToString());

                buffer = serializer.Serialize(new MessagePackPacket(packet));
                MessagePackPacket _packet = serializer.Deserialize<MessagePackPacket>(buffer);

                Assert.Equal(packet.PackType, _packet.PackType);
                Assert.Equal(packet.PacketType, _packet.PacketType);
                Assert.Equal(packet.FromId, _packet.FromId);

                _person = serializer.Deserialize<MessagePackMessagePack<MpPerson>>(_packet.Payload)?.Message;

                Assert.Equal(person.Id, _person.Id);
                Assert.Equal(person.Name, _person.Name);
            } 
#endif
        }
    }
}
