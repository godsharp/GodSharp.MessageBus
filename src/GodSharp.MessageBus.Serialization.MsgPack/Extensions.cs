using GodSharp.Bus.Messages.Abstractions;
using GodSharp.Bus.Messages.Serialization;
using GodSharp.Bus.Messages.Serialization.MsgPack;

namespace GodSharp.Bus.Messages
{
    public static class Extensions
    {
        public static IMessageBusBuilder AddMsgPack(this IMessageBusBuilder builder) => builder.AddSerializer<MsgPackSerializer>().AddSerializationAdapter<DefaultSerializationAdapter>();
    }
}
