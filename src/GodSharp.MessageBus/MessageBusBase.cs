using System;
using System.Collections.Concurrent;
using System.Linq;

namespace GodSharp.Bus.Messages
{
    using GodSharp.Bus.Messages.Internals;

    public abstract class MessageBusBase
    {
        ConcurrentDictionary<Type, object> contaners;

        public MessageBusBase()
        {
            contaners = new ConcurrentDictionary<Type, object>();
        }

        internal void Subscribe<TBusHanlder, TPackMessage,TMessage>(TBusHanlder handler, Func<IMessageTypeHandler<TBusHanlder, TPackMessage, TMessage>> fn = null, string name = "default") where TPackMessage : MessagePack
        {
            if (handler == null) return;

            Type type = typeof(TMessage);

            IMessageTypeHandler<TBusHanlder, TPackMessage, TMessage> typeHandler;
            if (!contaners.Keys.Contains(type))
            {
                typeHandler = fn();
                typeHandler.Add(handler, name);

                contaners.TryAdd(type, typeHandler);
                return;
            }

            if (!contaners.TryGetValue(type, out object val)) return;

            typeHandler = val as IMessageTypeHandler<TBusHanlder, TPackMessage, TMessage>;

            if (typeHandler == null) throw new InvalidOperationException($"Not found handler for {type}:{name}");

            typeHandler.Add(handler, name);
        }

        internal void Publish<TBusHanlder, TPackMessage, TMessage>(TPackMessage message) where TPackMessage : MessagePack
        {
            Type type = typeof(TMessage);

            if (!contaners.TryGetValue(type, out object val)) return;

            IMessageTypeHandler<TBusHanlder, TPackMessage, TMessage> handler = val as IMessageTypeHandler<TBusHanlder, TPackMessage, TMessage>;

            if (handler == null) throw new InvalidOperationException($"Not found handler for {type}:{message.Name}");

            handler.Execute(message);
        }

        internal void Unsubscribe<TBusHanlder, TPackMessage, TMessage>(TBusHanlder handler, string name = "default") where TPackMessage : MessagePack
        {
            if (handler == null) return;
            Type type = typeof(TMessage);

            if (!contaners.TryGetValue(type, out object val)) return;

            IMessageTypeHandler<TBusHanlder, TPackMessage, TMessage> typeHandler = val as IMessageTypeHandler<TBusHanlder, TPackMessage, TMessage>;

            if (typeHandler == null) throw new InvalidOperationException($"Not found handler for {type}:{name}");

            typeHandler.Remove(handler, name);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    try
                    {
                        Type[] types = contaners.Keys.ToArray();
                        foreach (var type in types)
                        {
                            if (!contaners.TryGetValue(type, out object val)) return;
                            IDisposable disposable = val as IDisposable;
                            if (disposable != null) disposable.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MessageTypeHandler() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
