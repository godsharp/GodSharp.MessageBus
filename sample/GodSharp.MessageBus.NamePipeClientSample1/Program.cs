using GodSharp.Bus.Messages;
using GodSharp.MessageBus.NamePipeSample;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GodSharp.MessageBus.NamePipeClientSample1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello GodSharp.MessageBus.NamePipeClientSample1!");

            NamedPipeTester.RunAsClient(() => {
                Bus.Messages.MessageBus.Subscribe(MessageHandler10);
                Bus.Messages.MessageBus.Subscribe(MessageHandler11, "handler1");
                Bus.Messages.MessageBus.Subscribe(MessageHandler12, "handler2");

                Bus.Messages.MessageBus.Subscribe<TestMessage>(MessageHandler20);
                Bus.Messages.MessageBus.Subscribe<TestMessage>(MessageHandler21, "handler1");
                Bus.Messages.MessageBus.Subscribe<TestMessage>(MessageHandler22, "handler2");
            });

            CancellationTokenSource cts = new CancellationTokenSource();

            Task task = new Task(async () =>
            {
                try
                {
                    Random random = new Random((int)DateTime.Now.Ticks);

                    while (!cts.IsCancellationRequested)
                    {
                        int i = random.Next(1, 7);
                        Console.WriteLine($"send1 {i}");

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
                                Bus.Messages.MessageBus.Publish(new TestMessage() { FromId = "001" });
                                break;
                            case 5:
                                Bus.Messages.MessageBus.Publish(new TestMessage() { FromId = "001" }, "handler1");
                                break;
                            case 6:
                                Bus.Messages.MessageBus.Publish(new TestMessage() { FromId = "001" }, "handler2");
                                break;
                            default:
                                continue;
                        }

                        await Task.Delay(10);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("task is cancel");
            });

            task.Start();
            NamedPipeTester.RunAsLoop();
        }

        static void MessageHandler10(MessagePack pack)
        {
            Console.WriteLine($"MessageHandler10 received message {pack.Name}/{pack.DateTime.ToString("HH:mm:ss.fff")}");
        }

        static void MessageHandler11(MessagePack pack)
        {
            Console.WriteLine($"MessageHandler11 received message {pack.Name}/{pack.DateTime.ToString("HH:mm:ss.fff")}");
        }

        static void MessageHandler12(MessagePack pack)
        {
            Console.WriteLine($"MessageHandler12 received message {pack.Name}/{pack.DateTime.ToString("HH:mm:ss.fff")}");
        }

        static void MessageHandler20(MessagePack<TestMessage> pack)
        {
            Console.WriteLine($"MessageHandler20 received message {pack.Name}/{pack.DateTime.ToString("HH:mm:ss.fff")}/{pack.Message.Id}/{pack.Message.FromId}");
        }

        static void MessageHandler21(MessagePack<TestMessage> pack)
        {
            Console.WriteLine($"MessageHandler21 received message {pack.Name}/{pack.DateTime.ToString("HH:mm:ss.fff")}/{pack.Message.Id}/{pack.Message.FromId}");
        }

        static void MessageHandler22(MessagePack<TestMessage> pack)
        {
            Console.WriteLine($"MessageHandler22 received message {pack.Name}/{pack.DateTime.ToString("HH:mm:ss.fff")}/{pack.Message.Id}/{pack.Message.FromId}");
        }
    }
}
