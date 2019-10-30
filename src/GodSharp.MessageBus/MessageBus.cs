using GodSharp.Bus.Messages.Abstractions;
using System;

namespace GodSharp.Bus.Messages
{
    public class MessageBus
    {
        public static IMessageBus Instance { get; internal set; }

        public static void Publish(string name = "default") => Instance.Publish(name);
        public static void Publish<T>(T message, string name = "default") => Instance.Publish<T>(message,name);
        public static void Subscribe(MessageBusHandler handler, string name = "default") => Instance.Subscribe(handler, name);
        public static void Subscribe<T>(MessageBusHandler<T> handler, string name = "default") => Instance.Subscribe<T>(handler, name);
        public static void Unsubscribe(MessageBusHandler handler, string name = "default") => Instance.Unsubscribe(handler, name);
        public static void Unsubscribe<T>(MessageBusHandler<T> handler, string name = "default") => Instance.Unsubscribe<T>(handler, name);

        public static void Initialize(Action<IMessageBusBuilder> builder) => builder.Invoke(new MessageBusBuilder());
        public static void Start(Action<IMessageBusOperator> @operator) => @operator.Invoke(new MessageBusOperator());
    }
}