using GodSharp.Bus.Messages.Abstractions;
using GodSharp.Bus.Messages.Serialization.Protobuf;

namespace GodSharp.Bus.Messages
{
    public static class Extensions
    {
        public static IMessageBusBuilder AddProtobuf(this IMessageBusBuilder builder)
        {
            builder.AddSerializer<ProtobufSerializer>().AddSerializationAdapter<ProtobufSerializationAdapter>();
            return builder;
        }
    }
}
