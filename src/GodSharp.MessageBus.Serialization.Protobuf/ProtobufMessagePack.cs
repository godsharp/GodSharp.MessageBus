using ProtoBuf;
using System;

namespace GodSharp.Bus.Messages.Serialization.Protobuf
{
    [ProtoContract]
    public class ProtobufMessagePack : MessagePack
    {
        [ProtoMember(1)]
        public override string Name { get => base.Name; set => base.Name = value; }
        [ProtoMember(2)]
        public override DateTime DateTime { get => base.DateTime; set => base.DateTime = value; }
        public ProtobufMessagePack()
        {
        }
    }

    [ProtoContract]
    public class ProtobufMessagePack<T> : MessagePack<T>
    {
        [ProtoMember(1)]
        public override string Name { get => base.Name; set => base.Name = value; }
        [ProtoMember(2)]
        public override DateTime DateTime { get => base.DateTime; set => base.DateTime = value; }
        [ProtoMember(3)]
        public override T Message { get => base.Message; set => base.Message = value; }

        public ProtobufMessagePack()
        {
        }
        public ProtobufMessagePack(T message, string name = "default") : base(message, name)
        {
        }
    }
}
