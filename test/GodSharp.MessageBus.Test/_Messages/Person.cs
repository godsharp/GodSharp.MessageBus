using ProtoBuf;
#if !NET40
using MessagePack;
#endif

namespace GodSharp.Bus.Messages.Tests._Messages
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public class Person
    {
        public int Id { get; set; } = 1;

        public string Name { get; set; } = "Tom";

        public Address Address { get; set; }
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public class Address
    {
        public int Id { get; set; } = 1;

        public string Name { get; set; } = "Hubei";
    }

#if !NET40
    [MessagePackObject]
    public class MpPerson
    {
        [Key(1)]
        public int Id { get; set; } = 1;

        [Key(2)]
        public string Name { get; set; } = "Tom";

        [Key(3)]
        public MpAddress Address { get; set; }
    }

    [MessagePackObject]
    public class MpAddress
    {
        [Key(1)]
        public int Id { get; set; } = 1;

        [Key(2)]
        public string Name { get; set; } = "Hubei";
    } 
#endif
}
