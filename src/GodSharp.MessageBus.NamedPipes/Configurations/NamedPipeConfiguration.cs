namespace GodSharp.Bus.Messages.Configurations
{
    internal class NamedPipeConfiguration
    {
        public static MessageBusNamedPipeServer NamedPipeServer { get; set; }

        public static void ServerInitialize()
        {
            NamedPipeServer = new MessageBusNamedPipeServer();
        }
    }
}
