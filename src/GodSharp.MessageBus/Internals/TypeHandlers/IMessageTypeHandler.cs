namespace GodSharp.Bus.Messages.Internals
{
    internal interface IMessageTypeHandler<TBusHanlder, TPackMessage, TMessage>
    {
        void Add(TBusHanlder handler, string name = "default");
        bool Contains(string name = "default");
        void Remove(TBusHanlder handler, string name = "default");
        void Execute(TPackMessage message);
    }
}