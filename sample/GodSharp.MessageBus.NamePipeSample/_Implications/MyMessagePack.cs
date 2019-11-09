using ProtoBuf;
using System;

namespace GodSharp.MessageBus.NamePipeSample
{
    [ProtoContract]
    //[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public class MyMessagePack : Bus.Messages.MessagePack
    {
        [ProtoMember(1)]
        public override string Name { get => base.Name; set => base.Name = value; }
        [ProtoMember(2)]
        public override DateTime DateTime { get => base.DateTime; set => base.DateTime = value; }
        public MyMessagePack()
        {
        }
    }

    [ProtoContract]
    //[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public class MyMessagePack<T> : Bus.Messages.MessagePack<T>
    {
        [ProtoMember(1)]
        public override string Name { get => base.Name; set => base.Name = value; }
        [ProtoMember(2)]
        public override DateTime DateTime { get => base.DateTime; set => base.DateTime = value; }
        [ProtoMember(3)]
        public override T Message { get => base.Message; set => base.Message = value; }

        public MyMessagePack()
        {
        }
        public MyMessagePack(T message, string name = "default") : base(message, name)
        {
        }
    }
}
