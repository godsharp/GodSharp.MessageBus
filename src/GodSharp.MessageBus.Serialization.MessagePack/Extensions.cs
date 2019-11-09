using GodSharp.Bus.Messages.Abstractions;
using GodSharp.Bus.Messages.Serialization.MessagePack;

namespace GodSharp.Bus.Messages
{
    public static class Extensions
    {
        public static IMessageBusBuilder AddMessagePack(this IMessageBusBuilder builder) => builder.AddSerializer<MessagePackSerializer>().AddSerializationAdapter<MessagePackSerializationAdapter>(); 
    }
}