using System;

namespace GodSharp.Bus.Messages
{
    public class MessagePack
    {
        public string Name { get; set; } = "default";
        public DateTime Datetime { get; set; } = DateTime.Now;

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
        public T Message { get; set; }
        
        public MessagePack(T message, string name = "default")
        {
            Name = name;
            Message = message;
        }
    }
}
