using GodSharp.Bus.Messages.Abstractions;
using GodSharp.Bus.Messages.Internals;
using GodSharp.Bus.Messages.Transfers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace GodSharp.Bus.Messages
{
    public class MessageBusRemote : MessageBusBase, IMessageBus, IMessageBusHanlder
    {
        ConcurrentDictionary<string, int> Publishs { get; set; }
        ConcurrentDictionary<string, List<string>> Clients { get; set; }

        public MessageBusRemote()
        {
            Publishs = new ConcurrentDictionary<string, int>();
            Clients = new ConcurrentDictionary<string, List<string>>();
        }

        public virtual void Subscribe(MessageBusHandler handler, string name = "default")
        {
            base.Subscribe<MessageBusHandler, MessagePack, NullMessage>(handler, () => new MessageTypeHandler(), name);

            SubscribeRemote(typeof(NullMessage).FullName, PacketType.Subscribe);
        }

        public virtual void Publish(string name = "default")
        {
            MessagePack pack = new MessagePack(name);
            if (Publishs.Keys.Contains(typeof(NullMessage).FullName))
            RemoteConfiguration.Transmitter.Send(new Packet(typeof(NullMessage).FullName,
                RemoteConfiguration.Adapter.MessagePackSerialize(pack, RemoteConfiguration.Serializer),
                PacketType.Publish,
                RemoteConfiguration.Transmitter.Id));

            base.Publish<MessageBusHandler, MessagePack, NullMessage>(pack);
        }

        public virtual void Unsubscribe(MessageBusHandler handler, string name = "default")
        {
            base.Unsubscribe<MessageBusHandler, MessagePack, NullMessage>(handler, name);

            SubscribeRemote(typeof(NullMessage).FullName, PacketType.Unsubscribe);
        }

        public virtual void Subscribe<T>(MessageBusHandler<T> handler, string name = "default")
        {
            base.Subscribe<MessageBusHandler<T>, MessagePack<T>, T>(handler, () => new MessageTypeHandler<T>(), name);

            SubscribeRemote(typeof(T).FullName, PacketType.Subscribe);
        }

        public virtual void Publish<T>(T message, string name = "default")
        {
            MessagePack<T> pack = new MessagePack<T>(message, name);
            if (Publishs.Keys.Contains(typeof(T).FullName)) 
            RemoteConfiguration.Transmitter.Send(new Packet(typeof(T).FullName,
                RemoteConfiguration.Adapter.MessagePackSerialize(pack, RemoteConfiguration.Serializer),
                PacketType.Publish, 
                RemoteConfiguration.Transmitter.Id));

            base.Publish<MessageBusHandler<T>, MessagePack<T>, T>(pack);
        }

        public virtual void Unsubscribe<T>(MessageBusHandler<T> handler, string name = "default")
        {
            base.Unsubscribe<MessageBusHandler<T>, MessagePack<T>, T>(handler, name);

            SubscribeRemote(typeof(T).FullName, PacketType.Unsubscribe);
        }

        private void SubscribeRemote(string type,PacketType packetType)
        {
            RemoteConfiguration.Transmitter.Send(new Packet(type, null, packetType, RemoteConfiguration.Transmitter.Id));
        }

        public void Join(Packet packet)
        {
            try
            {
                if (Clients.Keys.Contains(packet.FromId)) return;

                Clients.AddOrUpdate(packet.FromId, new List<string>());

                foreach (var item in Containers.Keys)
                {
                    SubscribeRemote(item.FullName, PacketType.Subscribe);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Exit(Packet packet)
        {
            try
            {
                List<string> types = null;
                if (Clients.ContainsKey(packet.FromId)) Clients.TryRemove(packet.FromId, out types);

                foreach (var item in types)
                {
                    Subscribe(item, false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                int val = 0;
                Publishs.TryGetValue(type, out val);

                Publishs.AddOrUpdate(type, val + 1);
            }
            else
            {
                if (Publishs.TryGetValue(type, out int val))
                {
                    Publishs.AddOrUpdate(type, val - 1);
                }

                if (val <= 1)
                {
                    Publishs.TryRemove(type, out int _);
                }
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
