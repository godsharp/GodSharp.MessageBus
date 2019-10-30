using GodSharp.Bus.Messages.Internals;
using GodSharp.Bus.Messages.Abstractions;
using System.Collections.Generic;
using GodSharp.Bus.Messages.Transfers;

namespace GodSharp.Bus.Messages
{
    public class MessageBusRemote : MessageBusBase, IMessageBus, IMessageBusHanlder
    {
        List<string> Publishs { get; set; }

        public MessageBusRemote()
        {
            Publishs = new List<string>();
        }

        public virtual void Subscribe(MessageBusHandler handler, string name = "default")
        {
            base.Subscribe<MessageBusHandler, MessagePack, NullMessage>(handler, () => new MessageTypeHandler(), name);

            RemoteConfiguration.Transmitter.Send(new Packet(typeof(NullMessage).FullName, null, PacketType.Subscribe, RemoteConfiguration.Transmitter.Id));
        }

        public virtual void Publish(string name = "default")
        {
            MessagePack pack = new MessagePack(name);
            if (Publishs.Contains(typeof(NullMessage).FullName))
            RemoteConfiguration.Transmitter.Send(new Packet(typeof(NullMessage).FullName,
                RemoteConfiguration.Adapter.MessagePackSerialize(pack, RemoteConfiguration.Serializer),
                PacketType.Publish,
                RemoteConfiguration.Transmitter.Id));

            base.Publish<MessageBusHandler, MessagePack, NullMessage>(pack);
        }

        public virtual void Unsubscribe(MessageBusHandler handler, string name = "default")
        {
            base.Unsubscribe<MessageBusHandler, MessagePack, NullMessage>(handler, name);

            RemoteConfiguration.Transmitter.Send(new Packet(typeof(NullMessage).FullName, null, PacketType.Unsubscribe, RemoteConfiguration.Transmitter.Id));
        }

        public virtual void Subscribe<T>(MessageBusHandler<T> handler, string name = "default")
        {
            base.Subscribe<MessageBusHandler<T>, MessagePack<T>, T>(handler, () => new MessageTypeHandler<T>(), name);

            RemoteConfiguration.Transmitter.Send(new Packet(typeof(T).FullName, null, PacketType.Subscribe, RemoteConfiguration.Transmitter.Id));
        }

        public virtual void Publish<T>(T message, string name = "default")
        {
            MessagePack<T> pack = new MessagePack<T>(message, name);
            if (Publishs.Contains(typeof(T).FullName)) 
            RemoteConfiguration.Transmitter.Send(new Packet(typeof(T).FullName,
                RemoteConfiguration.Adapter.MessagePackSerialize(pack, RemoteConfiguration.Serializer),
                PacketType.Publish, 
                RemoteConfiguration.Transmitter.Id));

            base.Publish<MessageBusHandler<T>, MessagePack<T>, T>(pack);
        }

        public virtual void Unsubscribe<T>(MessageBusHandler<T> handler, string name = "default")
        {
            base.Unsubscribe<MessageBusHandler<T>, MessagePack<T>, T>(handler, name);

            RemoteConfiguration.Transmitter.Send(new Packet(typeof(T).FullName, null, PacketType.Unsubscribe, RemoteConfiguration.Transmitter.Id));
        }

        public void Handle<T>(Packet packet)
        {
            switch (packet.PacketType)
            {
                case PacketType.Subscribe:
                    Subscribe(packet.PackType, true);
                    break;
                case PacketType.Unsubscribe:
                    Subscribe(packet.PackType, false);
                    break;
                case PacketType.Publish:
                    Publish<T>(packet.Payload);
                    break;
                default:
                    break;
            }
        }

        private void Subscribe(string type, bool subscribe = true)
        {
            if (subscribe)
            {
                if (!Publishs.Contains(type)) Publishs.Add(type);
            }
            else
            {
                if (Publishs.Contains(type)) Publishs.Remove(type);
            }
        }

        private void Publish<T>(byte[] buffer)
        {
            if (typeof(T) == typeof(NullMessage))
            {
                MessagePack tt = RemoteConfiguration.Adapter.MessagePackDeserialize(buffer, RemoteConfiguration.Serializer);
                if (tt != null) base.Publish<MessageBusHandler, MessagePack, NullMessage>(tt);
            }
            else
            {
                MessagePack<T> t = RemoteConfiguration.Adapter.MessagePackDeserialize<T>(buffer, RemoteConfiguration.Serializer);
                if (t != null) base.Publish<MessageBusHandler<T>, MessagePack<T>, T>(t);
            }
        }
    }
}
