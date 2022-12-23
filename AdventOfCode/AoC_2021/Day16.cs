using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day16 : IDay<char>
    {
        public static Packet MainPacket;
        static string BitString;

        public long A(List<char> inputs)
        {
            BuildPackets(inputs, out var packet);

            return SumVersions(packet);
        }

        public long SumVersions(Packet packet)
        {
            return packet.Version + packet.SubPackets.Sum(SumVersions);
        }

        public long B(List<char> inputs)
        {
            BuildPackets(inputs, out var packet);

            return HandlePacket(packet);
        }

        public long HandlePacket(Packet packet)
        {
            switch (packet.TypeId)
            {
                case 0: //sum
                    return packet.SubPackets.Sum(HandlePacket);
                case 1:
                    return packet.SubPackets.Aggregate(1L, (agg, next) => agg * HandlePacket(next));
                case 2:
                    return packet.SubPackets.Min(HandlePacket);
                case 3:
                    return packet.SubPackets.Max(HandlePacket);
                case 4:
                    return packet.Value.GetValueOrDefault();
                case 5:
                    return HandlePacket(packet.SubPackets[0]) > HandlePacket(packet.SubPackets[1]) ? 1 : 0;
                case 6:
                    return HandlePacket(packet.SubPackets[0]) < HandlePacket(packet.SubPackets[1]) ? 1 : 0;
                case 7:
                    return HandlePacket(packet.SubPackets[0]) == HandlePacket(packet.SubPackets[1]) ? 1 : 0;
                default:
                    break;
            }
            return default;
        }

        public List<char> SetupInputs(string[] inputs)
        {
            return inputs[0].Select(x => x).ToList();
        }

        public static void BuildPackets(List<char> inputs, out Packet packet)
        {
            if (MainPacket != null)
            {
                packet = MainPacket;
                return;
            }
            BitString = string.Join(null, inputs.Select(GetBitsFromHex));
            var index = 0;

            index = CreatePacket(index, out packet);
        }

        private static int HandleType(int i, int type, Packet packet)
        {
            switch (type)
            {
                case 4:
                    i = LiteralValue(i, packet);
                    break;
                default:
                    i = Operator(i, packet);
                    break;
            }
            return i;
        }

        private static int Operator(int i, Packet packet)
        {
            if (BitString[i++] == '1')  //next 11 bits - number of subpackets
            {
                var numberOfSubpackets = Convert.ToInt32(BitString[i..(i += 11)], 2);
                for (int n = 0; n < numberOfSubpackets; n++)
                {
                    i = CreatePacket(i, out var subPacket);
                    packet.SubPackets.Add(subPacket);
                }
            }
            else //next 15 bits - total length of subpacket bits
            {
                var numberOfBits = Convert.ToInt32(BitString[i..(i += 15)], 2);
                var offset = i;
                while (i < numberOfBits + offset)
                {
                    i = CreatePacket(i, out var subPacket);
                    packet.SubPackets.Add(subPacket);
                }
            }

            return i;
        }

        private static int LiteralValue(int i, Packet packet)
        {
            var valueBuffer = "";
            while (BitString[i++] == '1')
            {
                valueBuffer += BitString[i..(i += 4)];
            }
            valueBuffer += BitString[i..(i += 4)];

            packet.Value = Convert.ToInt64(valueBuffer, 2);
            return i;
        }

        private static int CreatePacket(int i, out Packet packet)
        {
            var version = Convert.ToInt32(BitString[i..(i += 3)], 2);
            var type = Convert.ToInt32(BitString[(i)..(i += 3)], 2);
            
            packet = new Packet();
            if (MainPacket == null)
            {
                MainPacket = packet;
            }

            packet.Version = version;
            packet.TypeId = type;

            return HandleType(i, type, packet);
        }

        public static string GetBitsFromHex(char hex)
        {
            return Convert.ToString(BitConverter.GetBytes(int.Parse(hex.ToString(), System.Globalization.NumberStyles.HexNumber)).First(), 2).PadLeft(4, '0');
        }
    }

    public class Packet
    {
        public int Version { get; set; }
        public int TypeId { get; set; }

        public long? Value { get; set; }

        private List<Packet>? _subPackets;
        public List<Packet> SubPackets { get => _subPackets ??= new (); set => _subPackets = value;}
    }
}
