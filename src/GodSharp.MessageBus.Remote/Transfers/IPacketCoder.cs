namespace GodSharp.Bus.Messages.Transfers
{
    public interface IPacketCoder
    {
        byte[] Encode(byte[] buffer);

        byte[][] Decode(byte[] buffer, out int lastIndex);
    }
}