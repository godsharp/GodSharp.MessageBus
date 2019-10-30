using GodSharp.Bus.Messages.Abstractions;
using GodSharp.Bus.Messages.Configurations;

namespace GodSharp.Bus.Messages
{
    public static class Extensions
    {
        public static IMessageBusBuilder AddNamedPipeClient(this IMessageBusBuilder builder) => builder.AddTransmitter<NamedPipeTransmitter>();

        public static IMessageBusBuilder AddNamedPipeClient(this IMessageBusBuilder builder, NamedPipeTransmitter transmitter) => builder.AddTransmitter(transmitter);

        public static IMessageBusOperator AddNamedPipeServer(this IMessageBusOperator @operator)
        {
            NamedPipeConfiguration.ServerInitialize();
            return @operator;
        }
    }
}
