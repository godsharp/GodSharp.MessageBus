namespace GodSharp.Bus.Messages.Abstractions
{
    using GodSharp.Bus.Messages.Internals;

    public class MessageBusLocal : MessageBusBase, IMessageBus
    {
        public virtual void Subscribe(MessageBusHandler handler, string name = "default") => base.Subscribe<MessageBusHandler, MessagePack, NullMessage>(handler, () => new MessageTypeHandler(), name);

        public virtual void Publish(string name = "default") => base.Publish<MessageBusHandler, MessagePack, NullMessage>(new MessagePack(name));

        public virtual void Unsubscribe(MessageBusHandler handler, string name = "default") => base.Unsubscribe<MessageBusHandler, MessagePack, NullMessage>(handler, name);

        public virtual void Subscribe<T>(MessageBusHandler<T> handler, string name = "default") => base.Subscribe<MessageBusHandler<T>, MessagePack<T>, T>(handler, () => new MessageTypeHandler<T>(), name);

        public virtual void Publish<T>(T message, string name = "default") => base.Publish<MessageBusHandler<T>, MessagePack<T>, T>(new MessagePack<T>(message, name));

        public virtual void Unsubscribe<T>(MessageBusHandler<T> handler, string name = "default") => base.Unsubscribe<MessageBusHandler<T>, MessagePack<T>, T>(handler, name);
    }
}