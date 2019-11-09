using GodSharp.Bus.Messages;
using System;

namespace GodSharp.MessageBus.NamePipeSample
{
    public class Handlers
    {
        public static void Subscribe(int num)
        {
            switch (num)
            {
                case 1:
                    Bus.Messages.MessageBus.Subscribe(MessageHandler10);
                    Bus.Messages.MessageBus.Subscribe<TestMessage>(MessageHandler20);
                    break;
                case 2:
                    Bus.Messages.MessageBus.Subscribe(MessageHandler11, "handler1");
                    Bus.Messages.MessageBus.Subscribe<TestMessage>(MessageHandler21, "handler1");
                    break;
                case 3:
                    Bus.Messages.MessageBus.Subscribe(MessageHandler12, "handler2");
                    Bus.Messages.MessageBus.Subscribe<TestMessage>(MessageHandler22, "handler2");
                    break;
                default:
                    break;
            }
        }

        public static void MessageHandler10(MessagePack pack)
        {
            Console.WriteLine($"MessageHandler10 received message {pack.Name}/{pack.DateTime.ToString("HH:mm:ss.fff")}");
        }

        public static void MessageHandler11(MessagePack pack)
        {
            Console.WriteLine($"MessageHandler11 received message {pack.Name}/{pack.DateTime.ToString("HH:mm:ss.fff")}");
        }

        public static void MessageHandler12(MessagePack pack)
        {
            Console.WriteLine($"MessageHandler12 received message {pack.Name}/{pack.DateTime.ToString("HH:mm:ss.fff")}");
        }

        public static void MessageHandler20(MessagePack<TestMessage> pack)
        {
            Console.WriteLine($"MessageHandler20 received message {pack.Name}/{pack.DateTime.ToString("HH:mm:ss.fff")}/{pack.Message.Id}/{pack.Message.FromId}");
        }

        public static void MessageHandler21(MessagePack<TestMessage> pack)
        {
            Console.WriteLine($"MessageHandler21 received message {pack.Name}/{pack.DateTime.ToString("HH:mm:ss.fff")}/{pack.Message.Id}/{pack.Message.FromId}");
        }

        public static void MessageHandler22(MessagePack<TestMessage> pack)
        {
            Console.WriteLine($"MessageHandler22 received message {pack.Name}/{pack.DateTime.ToString("HH:mm:ss.fff")}/{pack.Message.Id}/{pack.Message.FromId}");
        }
    }
}
