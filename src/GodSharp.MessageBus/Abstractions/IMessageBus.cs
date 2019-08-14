using System;

namespace GodSharp.Bus.Messages
{
    public delegate void MessageBusHandler<T>(MessagePack<T> message);

    public delegate void MessageBusHandler(MessagePack message);

    public interface IMessageBus : IDisposable
    {
        void Subscribe(MessageBusHandler handler, string name = "default");
        void Publish(string name = "default");
        void Unsubscribe(MessageBusHandler handler, string name = "default");

        void Subscribe<T>(MessageBusHandler<T> handler, string name = "default");
        void Publish<T>(T message, string name = "default");
        void Unsubscribe<T>(MessageBusHandler<T> handler, string name = "default");
    }
}