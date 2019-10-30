using GodSharp.Bus.Messages.Tests._Messages;
using GodSharp.Bus.Messages.Transfers;
using System.Collections.Generic;
using Xunit;

namespace GodSharp.Bus.Messages.Serialization
{
    public class PacketSerializationTest
    {
        [Fact]
        public void Coder_Test()
        {
            using (ProtobufSerializer serializer = new ProtobufSerializer())
            {
                Person person = new Person() { Id = 2, Name = "Jerry", Address = new Address() };

                byte[] buffer = serializer.Serialize(person);

                PacketCoder coder = new PacketCoder();
                byte[] _buffer = coder.Encode(buffer);

                List<byte> lst = new List<byte>();
                lst.AddRange(buffer);
                lst.AddRange(_buffer);
                lst.AddRange(_buffer);
                lst.AddRange(_buffer);
                lst.AddRange(buffer);

                Queue<byte[]> queue = new Queue<byte[]>();

                byte[] _buffer_ = lst?.ToArray();

                byte[][] __buffer = coder.Decode(_buffer_,out int _);

                foreach (var item in __buffer)
                {
                    Person _person = serializer.Deserialize<Person>(item);
                    System.Console.WriteLine($"Id:{_person.Id},Name:{_person.Name}");
                    Assert.Equal(person.Id, _person.Id);
                    Assert.Equal(person.Name, _person.Name);
                }

                System.Console.WriteLine(_buffer_?.Length);
            }
        }
    }
}
