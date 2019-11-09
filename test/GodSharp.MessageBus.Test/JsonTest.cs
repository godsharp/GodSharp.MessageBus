using GodSharp.Bus.Messages.Serialization.Protobuf;
using GodSharp.Bus.Messages.Tests._Messages;
using Swifter.Json;
using System;
using Xunit;

namespace GodSharp.Bus.Messages.Tests
{
    public class JsonTest
    {
        [Fact]
        public void Json_Serialize_Deserialize_Test()
        {
            using (ProtobufSerializer serializer = new ProtobufSerializer())
            {
                Person person = new Person() { Id = 2, Name = "Jerry" };

                byte[] buffer = serializer.Serialize(person);

                Person _person = serializer.Deserialize<Person>(buffer);

                Assert.Equal(person.Id, _person.Id);
                Assert.Equal(person.Name, _person.Name);

                JsonObject json = new JsonObject(typeof(Person).FullName, buffer);
                string jsonstr = JsonFormatter.SerializeObject(json);
                JsonObject _json = JsonFormatter.DeserializeObject<JsonObject>(jsonstr);

                JsonObject<Person> jsont = new JsonObject<Person>(typeof(Person).FullName, person);
                string jsontstr = JsonFormatter.SerializeObject(jsont);
                JsonObject<Person> _jsont = JsonFormatter.DeserializeObject<JsonObject<Person>>(jsontstr);
            }
        }

        class JsonObject
        {
            public string Type { get; set; }
            public byte[] Payload { get; set; }

            public JsonObject()
            {
            }

            public JsonObject(string type, byte[] payload)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
                Payload = payload ?? throw new ArgumentNullException(nameof(payload));
            }
        }

        class JsonObject<T>
        {
            public string Type { get; set; }
            public T Payload { get; set; }

            public JsonObject()
            {
            }

            public JsonObject(string type, T payload)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
                Payload = payload;
            }
        }
    }
}
