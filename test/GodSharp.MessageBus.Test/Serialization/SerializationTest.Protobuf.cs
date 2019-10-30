using GodSharp.Bus.Messages.Serialization;
using GodSharp.Bus.Messages.Tests._Messages;
using Xunit;

namespace GodSharp.Bus.Messages.Tests.Serialization
{
    public class SerializationTest
    {
        [Fact]
        public void Protobuf_Serialize_Deserialize_Test()
        {
            using (ProtobufSerializer serializer = new ProtobufSerializer())
            {
                Person person = new Person() { Id = 2, Name = "Jerry", Address=new Address() };

                //Packet<Person> packet = new MyPacket<Person>(new MessagePack<Person>(person), PacketTypes.Subscribe);

                byte[] buffer = serializer.Serialize(person);

                Person _person = serializer.Deserialize<Person>(buffer);

                Assert.Equal(person.Id, _person.Id);
                Assert.Equal(person.Name, _person.Name);

                //Packet<Person> _packet = serializer.Deserialize<Person>(buffer);
                //Person _person = _packet.Pack.Message;

                //Assert.Equal(person.Id, _person.Id);
                //Assert.Equal(person.Name, _person.Name);
            }
        }

        [Fact]
        public void Protobuf_Serialize_Deserialize_Cat_Test()
        {
            using (ProtobufSerializer serializer = new ProtobufSerializer())
            {
                Cat cat = new MyCat() { Id = 2, Name = "Jerry" };

                byte[] buffer = serializer.Serialize(cat);

                Cat _cat = serializer.Deserialize<MyCat>(buffer);

                Assert.Equal(cat.Id, _cat.Id);
                Assert.Equal(cat.Name, _cat.Name);

                //Packet<Person> _packet = serializer.Deserialize<Person>(buffer);
                //Person _person = _packet.Pack.Message;

                //Assert.Equal(person.Id, _person.Id);
                //Assert.Equal(person.Name, _person.Name);
            }
        }
    }
}
