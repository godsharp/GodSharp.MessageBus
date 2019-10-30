using System;

namespace GodSharp.Bus.Messages.Internals
{
    internal class MessageTypeHandler<T> : MessageTypeHandlerBase<MessageBusHandler<T>, MessagePack<T>, T>, 
        IMessageTypeHandler<MessageBusHandler<T>, MessagePack<T>, T>, IDisposable
    {
        public void Add(MessageBusHandler<T> handler, string name = "default") => base.Add((h1, h2) => h1 += h2, handler, name);

        public void Remove(MessageBusHandler<T> handler, string name = "default") => base.Remove((h1, h2) => h1 -= h2, handler, name);

        public void Execute(MessagePack<T> message) => base.Execute((h, m) => h.Invoke(m), message);

        public bool Contains(string name = "default") => base.Contains(name);
    }
}
