using GodSharp.Bus.Messages.Abstractions;
using ProtoBuf;
using System;

namespace GodSharp.MessageBus.NamePipeSample
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public class TestMessage: IMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FromId { get; set; }
    }
}
