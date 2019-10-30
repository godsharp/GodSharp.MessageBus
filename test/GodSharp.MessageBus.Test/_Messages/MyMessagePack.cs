namespace GodSharp.Bus.Messages.Tests._Messages
{
    public class MyMessagePack: MessagePack
    {
    }

    public class MyMessagePack<T> : MessagePack<T>
    {
        public MyMessagePack(T message, string name = "default") : base(message, name)
        {
        }
    }
}
