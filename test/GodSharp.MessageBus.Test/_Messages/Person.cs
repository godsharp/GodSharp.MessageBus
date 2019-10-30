using ProtoBuf;

namespace GodSharp.Bus.Messages.Tests._Messages
{
    //[ProtoContract]
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public class Person
    {
        //[ProtoMember(1)]
        public int Id { get; set; } = 1;

        //[ProtoMember(2)]
        public string Name { get; set; } = "Tom";

        public Address Address { get; set; }
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public class Address
    {
        public int Id { get; set; } = 1;

        public string Name { get; set; } = "Hubei";
    }
}
