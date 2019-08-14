using System;
using System.Collections.Concurrent;
using System.Linq;

namespace GodSharp.Bus.Messages
{
    internal abstract class MessageTypeHandlerBase<TBusHandler,TPackMessage,TMessage> : IDisposable
        where TBusHandler : Delegate
        where TPackMessage:MessagePack
    {
        ConcurrentDictionary<string, TBusHandler> handlers;
        
        public MessageTypeHandlerBase()
        {
            handlers = new ConcurrentDictionary<string, TBusHandler>();
        }

        public void Add(Action<TBusHandler, TBusHandler> @operator, TBusHandler handler, string name = "default")
        {
            if (!handlers.ContainsKey(name))
            {
                handlers.TryAdd(name, handler);
                return;
            }

            if (!handlers.TryGetValue(name, out TBusHandler _handler)) return;

            Delegate[] delegates = _handler.GetInvocationList();

            if (delegates.Any(x => x.Target == handler.Target && x.Method == handler.Method))
            {
#if DEBUG
                Console.WriteLine($"add same:{handler.Target.GetType().Name}.{handler.Method}");
#endif
                return;
            }
            Console.WriteLine($"add:{handler.Target.GetType().Name}.{handler.Method}");

            @operator(_handler, handler);
            //_handler += handler;
        }

        public void Remove(Action<TBusHandler, TBusHandler> @operator, TBusHandler handler, string name = "default")
        {
            if (!handlers.ContainsKey(name)) return;
            if (!handlers.TryGetValue(name, out TBusHandler _handler)) return;

            Delegate[] delegates = _handler.GetInvocationList();

            if (delegates.Any(x => x.Target == handler.Target && x.Method == handler.Method))
            {
#if DEBUG
                Console.WriteLine($"remove:{handler.Target.GetType().Name}.{handler.Method}");
#endif
                @operator(_handler, handler);
                //_handler -= handler;
            }
        }

        public void Execute(Action<TBusHandler, TPackMessage> @operator, TPackMessage message)
        {
            if (message == null) return;
            if (!handlers.ContainsKey(message.Name)) return;
            if (!handlers.TryGetValue(message.Name, out TBusHandler _handler)) return;

            @operator(_handler, message);
            //_handler(message);
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
                    if (handlers == null || handlers.Count == 0) return;
                    handlers.Clear();
                    handlers = null;
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
