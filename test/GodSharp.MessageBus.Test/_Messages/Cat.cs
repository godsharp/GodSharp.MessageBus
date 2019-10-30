using ProtoBuf;

namespace GodSharp.Bus.Messages.Tests._Messages
{
    public class Cat
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public class MyCat : Cat
    {
        public override int Id { get; set; }
        public override string Name { get; set; }
    }
}