using GodSharp.Bus.Messages.Abstractions;

namespace GodSharp.Bus.Messages
{
    public static class Extensions
    {
        public static IMessageBusBuilder AddMemory(this IMessageBusBuilder builder)
        {
            MessageBus.Instance = new MessageBusLocal();
            return builder;
        }
    }
}
