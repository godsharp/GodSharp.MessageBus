using GodSharp.Pipes.NamedPipes;
using System.Collections.Concurrent;

namespace GodSharp.Bus.Messages
{
    internal class MessageBusNamedPipeServer
    {
        NamedPipeServer Server = null;

        public MessageBusNamedPipeServer()
        {
            Server = new NamedPipeServer(new NamedPipeServerOptions("godhsarp.bus.message.transmitter.namedpipe", OnReadCompleted, OnConnectionCompleted, OnStopCompleted, OnException, OnOutputLogging, 1024, 254));

            Server.Start();
        }

        private void OnReadCompleted(NamedPipeConnectionArgs args)
        {
            Server.Write(args.Buffer);
        }

        private void OnConnectionCompleted(NamedPipeConnectionArgs args)
        {
            //System.Console.WriteLine($"");
        }

        private void OnStopCompleted(NamedPipeConnectionArgs args)
        {
        }

        private void OnException(NamedPipeConnectionArgs args)
        {

        }
        private void OnOutputLogging(string log)
        {
            System.Console.WriteLine(log);
        }
    }
}
