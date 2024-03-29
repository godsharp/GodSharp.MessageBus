﻿using GodSharp.Bus.Messages.Serialization.Protobuf;
using GodSharp.Bus.Messages.Tests._Messages;
using GodSharp.Bus.Messages.Transfers;
using System;
using Xunit;

namespace GodSharp.Bus.Messages.Tests.Serialization
{
    public partial class SerializationTest
    {
        [Fact]
        public void Protobuf_Serialize_Deserialize_Test()
        {
            using (ProtobufSerializer serializer = new ProtobufSerializer())
            {
                Person person = new Person() { Id = 2, Name = "Jerry", Address = new Address() };

                byte[] buffer = serializer.Serialize(person);

                Person _person = serializer.Deserialize<Person>(buffer);

                Assert.Equal(person.Id, _person.Id);
                Assert.Equal(person.Name, _person.Name);

                ProtobufMessagePack<Person> pack = new ProtobufMessagePack<Person>(person);

                buffer = serializer.Serialize(pack);
                _person = serializer.Deserialize<ProtobufMessagePack<Person>>(buffer)?.Message;

                Assert.Equal(person.Id, _person.Id);
                Assert.Equal(person.Name, _person.Name);

                Packet packet = new Packet(typeof(Person).FullName, buffer, PacketType.Join, Guid.NewGuid().ToString());

                buffer = serializer.Serialize(new ProtobufPacket(packet));
                ProtobufPacket _packet = serializer.Deserialize<ProtobufPacket>(buffer);

                Assert.Equal(packet.PackType, _packet.PackType);
                Assert.Equal(packet.PacketType, _packet.PacketType);
                Assert.Equal(packet.FromId, _packet.FromId);

                _person = serializer.Deserialize<ProtobufMessagePack<Person>>(_packet.Payload)?.Message;

                Assert.Equal(person.Id, _person.Id);
                Assert.Equal(person.Name, _person.Name);
            }
        }
    }
}
