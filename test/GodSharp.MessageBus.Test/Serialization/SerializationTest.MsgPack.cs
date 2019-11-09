using GodSharp.Bus.Messages.Serialization.MsgPack;
using GodSharp.Bus.Messages.Tests._Messages;
using GodSharp.Bus.Messages.Transfers;
using System;
using Xunit;

namespace GodSharp.Bus.Messages.Tests.Serialization
{
    public partial class SerializationTest
    {
        [Fact]
        public void MsgPack_Serialize_Deserialize_Test()
        {
            using (MsgPackSerializer serializer = new MsgPackSerializer())
            {
                Person person = new Person() { Id = 2, Name = "Jerry", Address = new Address() };

                byte[] buffer = serializer.Serialize(person);

                Person _person = serializer.Deserialize<Person>(buffer);

                Assert.Equal(person.Id, _person.Id);
                Assert.Equal(person.Name, _person.Name);

                MessagePack<Person> pack = new MessagePack<Person>(person);

                buffer = serializer.Serialize(pack);
                _person = serializer.Deserialize<MessagePack<Person>>(buffer)?.Message;

                Assert.Equal(person.Id, _person.Id);
                Assert.Equal(person.Name, _person.Name);

                Packet packet = new Packet(typeof(Person).FullName, buffer, PacketType.Join, Guid.NewGuid().ToString());

                buffer = serializer.Serialize(packet);
                Packet _packet = serializer.Deserialize<Packet>(buffer);

                Assert.Equal(packet.PackType, _packet.PackType);
                Assert.Equal(packet.PacketType, _packet.PacketType);
                Assert.Equal(packet.FromId, _packet.FromId);

                _person = serializer.Deserialize<MessagePack<Person>>(_packet.Payload)?.Message;

                Assert.Equal(person.Id, _person.Id);
                Assert.Equal(person.Name, _person.Name);
            }
        }
    }
}
