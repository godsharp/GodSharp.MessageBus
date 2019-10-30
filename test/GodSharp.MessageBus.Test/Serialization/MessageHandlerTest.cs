using GodSharp.Bus.Messages.Tests._Messages;
using Xunit;

namespace GodSharp.Bus.Messages.Serialization
{
    public class MessageHandlerTest
    {
        [Fact]
        public void Handler_Register_Test()
        {
            //MessageHandler.Register<Cat>();
            //MessageHandler.Register(typeof(Person));

            //using (ProtobufSerializer serializer = new ProtobufSerializer())
            //{
            //    Person person = new Person() { Id = 2, Name = "Jerry", Address = new Address() };

            //    //Packet<Person> packet = new MyPacket<Person>(new MessagePack<Person>(person), PacketTypes.Subscribe);

            //    byte[] buffer = serializer.Serialize(person);

            //    Person _person = serializer.Deserialize<Person>(buffer);

            //    Assert.Equal(person.Id, _person.Id);
            //    Assert.Equal(person.Name, _person.Name);

            //    //Packet<Person> _packet = serializer.Deserialize<Person>(buffer);
            //    //Person _person = _packet.Pack.Message;

            //    //Assert.Equal(person.Id, _person.Id);
            //    //Assert.Equal(person.Name, _person.Name);
            //}
        }
    }
}
