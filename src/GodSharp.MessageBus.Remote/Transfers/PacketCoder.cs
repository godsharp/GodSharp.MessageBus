using System;
using System.Collections.Generic;

namespace GodSharp.Bus.Messages.Transfers
{
    public class PacketCoder : IPacketCoder
    {
        /*
         * STX: 02 00
         * Length:00 00 00 00
         * Content:00...00
         * ETX:00 03
         */

        public byte[] Encode(byte[] buffer)
        {
            List<byte> list = new List<byte>(buffer.Length + 8);
            list.Add(0x02);
            list.Add(0x00);
            list.AddRange(BitConverter.GetBytes(buffer.Length));
            list.AddRange(buffer);
            list.Add(0x00);
            list.Add(0x03);

            return list.ToArray();
        }

        public byte[][] Decode(byte[] buffer,out int lastIndex)
        {
            int index = 0;
            int used = 0;
            lastIndex = 0;
            List<byte[]> list = null;

            do
            {
                int position = Array.IndexOf<byte>(buffer, 0x02, index);
                if (position < 0 || position >= buffer.Length) break;

                if (buffer[position + 1] != 0x00)
                {
                    index = ++position;
                    continue;
                }

                if (buffer.Length - 1 - position < 4)
                {
                    index = ++position;
                    continue;
                }

                int length = BitConverter.ToInt32(buffer, position + 2);

                if (buffer.Length - (position + length + 8) < 0)
                {
                    index = ++position;
                    continue;
                }

                int _position = position + length + 6;

                if (buffer[_position] != 0x00 || buffer[_position + 1] != 0x03)
                {
                    index = ++position;
                    continue;
                }

                byte[] _buffer = new byte[length];

                Buffer.BlockCopy(buffer, position + 6, _buffer, 0, length);

                if (list == null) list = new List<byte[]>();
                list.Add(_buffer);

                index = _position + 2;
                used = index;

            } while (index < buffer.Length && index > -1);

            lastIndex = used;

            if (used > 0)
            {
                if (buffer.Length == used)
                {
                    buffer = null;
                }
                else
                {
                    byte[] tmp = new byte[buffer.Length - used];
                    Buffer.BlockCopy(buffer, used, tmp, 0, tmp.Length);
                    buffer = tmp;
                }
            }

            //ArraySegment<byte[]> segment = new ArraySegment<byte[]>(list.ToArray());
            
            return list?.ToArray();
        }
    }
}
