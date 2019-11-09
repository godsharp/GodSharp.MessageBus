using GodSharp.Bus.Messages;
using GodSharp.Bus.Messages.Serialization;
using GodSharp.Bus.Messages.Serialization.Protobuf;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GodSharp.MessageBus.NamePipeSample
{
    public class NamedPipeTester
    {
        private static void Initialize()
        {
            Bus.Messages.MessageBus.Initialize(x =>
            {
                x.AddRemote()
                .AddProtobuf()
                .AddNamedPipeClient()
                .AddMessageTypes();
            });
        }

        public static void RunAsServer()
        {
            Initialize();
            Bus.Messages.MessageBus.Start(x =>
            {
                x.AddNamedPipeServer();
            });

            RunAsLoopInternal();
        }

        public static void RunAsClient(int id)
        {
            Console.WriteLine($"Hello GodSharp.MessageBus.NamePipeClientSample:{id}!");

            //Console.WriteLine("Waitting...");
            //Console.ReadLine();

            Initialize();
            Bus.Messages.MessageBus.Start(x =>
            {
                x.StartTransmitter();
            });
            Handlers.Subscribe(id);

            Send();

            RunAsLoopInternal(CommandHandle);
        }

        private static void CommandHandle(string cmd)
        {
        }

        private static void RunAsLoopInternal(Action<string> action = null)
        {
            string str = null;

            do
            {
                str = Console.ReadLine();
                if (string.IsNullOrEmpty(str)) continue;
                action?.Invoke(str);
            } while (str != "q");

            cts?.Cancel();
            Console.WriteLine("task is canceling...");
            Console.ReadLine();
        }

        static Task task = null;
        static CancellationTokenSource cts = null;
        public static void Send()
        {
            cts = new CancellationTokenSource();

            task = new Task(async () =>
            {
                try
                {
                    Random random = new Random((int)DateTime.Now.Ticks);

                    while (!cts.IsCancellationRequested)
                    {
                        int i = random.Next(1, 7);
                        Console.WriteLine($"send3 {i}");

                        switch (i)
                        {
                            case 1:
                                Bus.Messages.MessageBus.Publish();
                                break;
                            case 2:
                                Bus.Messages.MessageBus.Publish("handler1");
                                break;
                            case 3:
                                Bus.Messages.MessageBus.Publish("handler2");
                                break;
                            case 4:
                                Bus.Messages.MessageBus.Publish(new TestMessage() { FromId = "003" });
                                break;
                            case 5:
                                Bus.Messages.MessageBus.Publish(new TestMessage() { FromId = "003" }, "handler1");
                                break;
                            case 6:
                                Bus.Messages.MessageBus.Publish(new TestMessage() { FromId = "003" }, "handler2");
                                break;
                            default:
                                continue;
                        }

                        await Task.Delay(1000);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("task is canceled");
            });

            task.Start();
        }
    }
}
