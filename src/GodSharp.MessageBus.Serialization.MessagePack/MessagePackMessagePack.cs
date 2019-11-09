using System;
using Mp = MessagePack;

namespace GodSharp.Bus.Messages.Serialization.MessagePack
{
    [Mp.MessagePackObject]
    public class MessagePackMessagePack : Messages.MessagePack
    {
        [Mp.Key(1)]
        public override string Name { get => base.Name; set => base.Name = value; }

        [Mp.Key(2)]
        public override DateTime DateTime { get => base.DateTime; set => base.DateTime = value; }
        public MessagePackMessagePack()
        {
        }
    }

    [Mp.MessagePackObject]
    public class MessagePackMessagePack<T> : Messages.MessagePack<T>
    {
        [Mp.Key(1)]
        public override string Name { get => base.Name; set => base.Name = value; }
        [Mp.Key(2)]
        public override DateTime DateTime { get => base.DateTime; set => base.DateTime = value; }
        [Mp.Key(3)]
        public override T Message { get => base.Message; set => base.Message = value; }

        public MessagePackMessagePack()
        {
        }
        public MessagePackMessagePack(T message, string name = "default") : base(message, name)
        {
        }
    }
}
