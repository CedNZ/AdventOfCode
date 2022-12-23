using AdventOfCodeCore;

namespace AoC_2022
{
    public class Day13 : IDay<Packet>
    {
        public long A(List<Packet> inputs)
        {
            int index = 0;
            List<int> correctIndexes = new();
            foreach (var pair in inputs.Chunk(2))
            {
                var left = pair[0];
                var right = pair[1];
                index++;
                if (ComparePackets(left, right).GetValueOrDefault())
                {
                    correctIndexes.Add(index);
                }
            }
            return correctIndexes.Sum();
        }

        public bool? ComparePackets(Packet left, Packet right)
        {
            for (var i = 0; i < Math.Max(left.Items.Count(), right.Items.Count()); i++)
            {
                if (i >= left.Items.Count() || i >= right.Items.Count())
                {
                    return i >= left.Items.Count();
                }

                var itemLeft = left.Items[i];
                var itemRight = right.Items[i];

                if (itemLeft.Number.HasValue && itemRight.Number.HasValue)
                {
                    if (itemLeft.Number < itemRight.Number)
                    {
                        return true;
                    }
                    else if (itemLeft.Number > itemRight.Number)
                    {
                        return false;
                    }
                }
                else if (itemLeft.Packet is not null && itemRight.Packet is not null)
                {
                    if (ComparePackets(itemLeft.Packet, itemRight.Packet) is bool b)
                    {
                        return b;
                    }
                }
                else
                {
                    var pL = itemLeft.Packet;
                    if (itemLeft.Number.HasValue)
                    {
                        pL = new Packet
                        {
                            Items = new()
                            {
                                (itemLeft.Number, null)
                            }
                        };
                        if (i < left.Items.Count - 1 && left.Items[i + 1].Packet is Packet l)
                        {
                            pL.Items.Add((null, l));
                        }
                    }
                    var pR = itemRight.Packet;
                    if (itemRight.Number.HasValue)
                    {
                        pR = new Packet
                        {
                            Items = new()
                            {
                                (itemRight.Number, null)
                            }
                        };
                        if (i < right.Items.Count - 1 && right.Items[i + 1].Packet is Packet r)
                        {
                            pR.Items.Add((null, r));
                        }
                    }

                    if (ComparePackets(pL, pR) is bool b)
                    {
                        return b;
                    }
                }
            }
            return null;
        }

        public long B(List<Packet> inputs)
        {
            var div2 = BuildPacket("[[2]]");
            var div6 = BuildPacket("[[6]]");

            inputs.Add(div2);
            inputs.Add(div6);

            var packetComparer = Comparer<Packet>.Create((p1, p2) => ComparePackets(p1, p2).GetValueOrDefault() ? -1 : 1);

            var ordered = inputs.OrderBy(x => x, packetComparer).ToList();

            return (ordered.IndexOf(div2) + 1) * (ordered.IndexOf(div6) + 1);
        }

        public (Packet, Packet) OrderPackets(Packet left, Packet right)
        {
            return ComparePackets(left, right).GetValueOrDefault()
                ? (left, right)
                : (right, left);
        }

        public List<Packet> SetupInputs(string[] inputs)
        {
            List<Packet> packets = new();
            foreach (var packetString in inputs.Where(x => x != ""))
            {
                packets.Add(BuildPacket(packetString));
            }
            return packets;
        }

        Packet BuildPacket(string packetString)
        {
            Packet mainPacket = new Packet();
            Packet currentPacket = mainPacket;

            for (int i = 0; i < packetString.Length; i++)
            {
                var c = packetString[i];
                switch (c)
                {
                    case ',':
                        continue;
                    case '[':
                        var subpacket = new Packet();
                        subpacket.ParentPacket = currentPacket;
                        currentPacket.Items.Add((null, subpacket));
                        currentPacket = subpacket;
                        continue;
                    case ']':
                        currentPacket = currentPacket.ParentPacket;
                        continue;
                }
                int num = 0;
                while (int.TryParse(packetString[i++].ToString(), out var x))
                {
                    num += x;
                    num *= 10;
                }
                num /= 10;
                currentPacket.Items.Add((num, null));
                i--;
            }
            foreach (var c in packetString)
            {
            }

            return mainPacket;
        }
    }
    public class Packet
    {
        public Packet? ParentPacket { get; set; }
        public List<(int? Number, Packet? Packet)> Items { get; set; } = new();

        public override string ToString()
        {
            return string.Join("", Items.Select(x => (x.Number.HasValue ? $"{x.Number.Value}," : $"[{x.Packet}]")));
        }
    }
}
