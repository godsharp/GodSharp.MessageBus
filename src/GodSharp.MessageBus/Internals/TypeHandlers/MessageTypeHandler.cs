using System;

namespace GodSharp.Bus.Messages.Internals
{
    internal class MessageTypeHandler : MessageTypeHandlerBase<MessageBusHandler, MessagePack, NullMessage>, 
        IMessageTypeHandler<MessageBusHandler, MessagePack, NullMessage>, IDisposable
    {
        public void Add(MessageBusHandler handler, string name = "default") => base.Add((h1, h2) => h1 += h2, handler, name);

        public void Remove(MessageBusHandler handler, string name = "default") => base.Remove((h1, h2) => h1 -= h2, handler, name);

        public void Execute(MessagePack message) => base.Execute((h, m) => h.Invoke(m), message);

        public bool Contains<TBusHanlder>(string name = "default") => base.Contains(name);
    }
}
