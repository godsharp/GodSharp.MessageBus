using GodSharp.Bus.Messages;
using GodSharp.Bus.Messages.Serialization;
using System;

namespace GodSharp.MessageBus.NamePipeSample
{
    public class NamedPipeTester
    {
        private static void Initialize()
        {
            Bus.Messages.MessageBus.Initialize(x =>
            {
                x.AddRemote()
                .AddSerializer<ProtobufSerializer>()
                .AddSerializationAdapter<SerializationAdapter>()
                .AddNamedPipeClient()
                .AddMessageTypes()
                .AddCoder();
            });
        }

        public static void RunAsServer()
        {
            Initialize();
            Bus.Messages.MessageBus.Start(x =>
            {
                x.AddNamedPipeServer();
            });
        }

        public static void RunAsClient(Action action = null)
        {
            Initialize();
            Bus.Messages.MessageBus.Start(x =>
            {
                x.StartTransmitter();
            });
            action?.Invoke();
        }

        public static void RunAsLoop()
        {
            string str = null;

            do
            {
                str = Console.ReadLine();
                if (string.IsNullOrEmpty(str)) continue;

            } while (str!="q");
        }
    }
}
