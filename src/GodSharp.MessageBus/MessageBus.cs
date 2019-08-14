namespace GodSharp.Bus.Messages
{
    using GodSharp.Bus.Messages.Internals;

    public class MessageBus : MessageBusBase, IMessageBus
    {
        public void Subscribe(MessageBusHandler handler, string name = "default") => base.Subscribe<MessageBusHandler, MessagePack, NullMessage>(handler, () => new MessageTypeHandler(), name);

        public void Publish(string name = "default") => base.Publish<MessageBusHandler, MessagePack, NullMessage>(new MessagePack(name));

        public void Unsubscribe(MessageBusHandler handler, string name = "default") => base.Unsubscribe<MessageBusHandler, MessagePack, NullMessage>(handler, name);

        public void Subscribe<T>(MessageBusHandler<T> handler, string name = "default") => base.Subscribe<MessageBusHandler<T>, MessagePack<T>, T>(handler, () => new MessageTypeHandler<T>(), name);

        public void Publish<T>(T message, string name = "default") => base.Publish<MessageBusHandler<T>, MessagePack<T>, T>(new MessagePack<T>(message, name));

        public void Unsubscribe<T>(MessageBusHandler<T> handler, string name = "default") => base.Unsubscribe<MessageBusHandler<T>, MessagePack<T>, T>(handler, name);
    }
}
