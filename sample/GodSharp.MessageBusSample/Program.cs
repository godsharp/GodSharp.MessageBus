using GodSharp.Bus.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GodSharp.MessageBusSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello GodSharp.MessageBus!");

            string cmd =null;

            IMessageBus bus = new MessageBus();

            bus.Subscribe(MessageHandler10);
            bus.Subscribe(MessageHandler11, "handler1");
            bus.Subscribe(MessageHandler12, "handler2");

            bus.Subscribe<TestMessage>(MessageHandler20);
            bus.Subscribe<TestMessage>(MessageHandler21, "handler1");
            bus.Subscribe<TestMessage>(MessageHandler22, "handler2");

            CancellationTokenSource cts = new CancellationTokenSource();

            Task task = new Task(async() =>
            {
                try
                {
                    Random random = new Random((int)DateTime.Now.Ticks);

                    while (!cts.IsCancellationRequested)
                    {
                        int i = random.Next(1, 7);
                        Console.WriteLine($"send {i}");

                        switch (i)
                        {
                            case 1:
                                bus.Publish();
                                break;
                            case 2:
                                bus.Publish("handler1");
                                break;
                            case 3:
                                bus.Publish("handler2");
                                break;
                            case 4:
                                bus.Publish(new TestMessage());
                                break;
                            case 5:
                                bus.Publish(new TestMessage(), "handler1");
                                break;
                            case 6:
                                bus.Publish(new TestMessage(), "handler2");
                                break;
                            default:
                                continue;
                        }

                        await Task.Delay(20);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("task is cancel");
            });

            task.Start();

            do
            {
                cmd = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(cmd)) continue;
            } while (cmd?.ToLower() != "q");

            Console.WriteLine("waitting to cancel");
            cts.Cancel();

            Console.ReadLine();
        }

        static void MessageHandler10(MessagePack pack)
        {
            Console.WriteLine($"MessageHandler10 received message {pack.Name}/{pack.Datetime.ToString("HH:mm:ss.fff")}");
        }

        static void MessageHandler11(MessagePack pack)
        {
            Console.WriteLine($"MessageHandler11 received message {pack.Name}/{pack.Datetime.ToString("HH:mm:ss.fff")}");
        }

        static void MessageHandler12(MessagePack pack)
        {
            Console.WriteLine($"MessageHandler12 received message {pack.Name}/{pack.Datetime.ToString("HH:mm:ss.fff")}");
        }

        static void MessageHandler20(MessagePack<TestMessage> pack)
        {
            Console.WriteLine($"MessageHandler20 received message {pack.Name}/{pack.Datetime.ToString("HH:mm:ss.fff")}/{pack.Message.Id}");
        }

        static void MessageHandler21(MessagePack<TestMessage> pack)
        {
            Console.WriteLine($"MessageHandler21 received message {pack.Name}/{pack.Datetime.ToString("HH:mm:ss.fff")}/{pack.Message.Id}");
        }

        static void MessageHandler22(MessagePack<TestMessage> pack)
        {
            Console.WriteLine($"MessageHandler22 received message {pack.Name}/{pack.Datetime.ToString("HH:mm:ss.fff")}/{pack.Message.Id}");
        }

        class TestMessage
        {
            public Guid Id { get; set; } = Guid.NewGuid();
        }
    }
}
