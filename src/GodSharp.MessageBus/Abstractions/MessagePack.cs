using System;

namespace GodSharp.Bus.Messages
{
    public class MessagePack
    {
        public virtual string Name { get; set; } = "default";
        public virtual DateTime DateTime { get; set; } = DateTime.Now;

        public MessagePack()
        {
        }

        public MessagePack(string name = "default")
        {
            Name = name;
        }
    }

    public class MessagePack<T> : MessagePack
    {
        public virtual T Message { get; set; }

        public MessagePack()
        {
        }

        public MessagePack(T message, string name = "default")
        {
            Name = name;
            Message = message;
        }
    }
}
