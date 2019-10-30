using GodSharp.Bus.Messages.Transfers;

using GodSharp.Pipes.NamedPipes;

namespace GodSharp.Bus.Messages
{
    public class NamedPipeTransmitter : Transmitter
    {
        NamedPipeClient pipe = new NamedPipeClient();

        public override string Id => pipe.Guid.ToString();

        public NamedPipeTransmitter()
        {
            pipe = new NamedPipeClient(new NamedPipeClientOptions("godhsarp.bus.message.transmitter.namedpipe", OnReadCompleted, OnConnectionCompleted, OnStopCompleted, OnException, OnOutputLogging, 1024));
        }

        public override void Send(byte[] buffer)
        {
            if (pipe.IsConnected) pipe.Write(buffer);
        }

        private void OnReadCompleted(NamedPipeConnectionArgs args)
        {
            OnReceived(args.Buffer);
        }

        private void OnConnectionCompleted(NamedPipeConnectionArgs args)
        {
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

        public override void Start()
        {
            pipe?.Start();
        }

        public override void Stop()
        {
            pipe?.Stop();
        }
    }
}
