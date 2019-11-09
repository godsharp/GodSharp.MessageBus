using GodSharp.Bus.Messages.Transfers;
using GodSharp.Pipes.NamedPipes;

namespace GodSharp.Bus.Messages
{
    internal class MessageBusNamedPipeServer
    {
        NamedPipeServer Server = null;

        public MessageBusNamedPipeServer()
        {
            Server = new NamedPipeServer(new NamedPipeServerOptions("godhsarp.bus.message.transmitter.namedpipe", OnReadCompleted, OnConnectionCompleted, OnInteractionCompleted, OnStopCompleted, OnException, OnOutputLogging, 1024, 254));

            Server.Start();
        }

        private void OnReadCompleted(MasterConnectionArgs args)
        {
            Server.Write(args.Buffer);
        }

        private void OnConnectionCompleted(MasterConnectionArgs args)
        {
        }

        private void OnInteractionCompleted(MasterConnectionArgs args)
        {
            RemoteConfiguration.Transmitter.Send(new Packet("Null", null, PacketType.Join, args.ClientGuid.ToString()));
        }

        private void OnStopCompleted(MasterConnectionArgs args)
        {
            RemoteConfiguration.Transmitter.Send(new Packet("Null", null, PacketType.Exit, args.ClientGuid.ToString()));
        }

        private void OnException(MasterConnectionArgs args)
        {

        }
        private void OnOutputLogging(string log)
        {
            System.Console.WriteLine(log);
        }
    }
}
