using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022
{
    public class Day13 : ISolve
    {
        public static string Solve1(string input)
        {
            var items = input.Split(Environment.NewLine)
                .Chunk(3)
                .Select(x => (Left: Packet.Parse(x[0]), Right: Packet.Parse(x[1])))
                .ToArray();

            //foreach (var item in items)
            //{
            //    Console.WriteLine(item.Left);
            //    Console.WriteLine(item.Right);
            //    Console.WriteLine();
            //}

            var sum = items
                .Select((x, i) => (Number: i + 1, Item: x))
                .Where(x => x.Item.Left.Compare(x.Item.Right) is 1)
                .Sum(x => x.Number);

            return sum.ToString();
        }

        public static string Solve2(string input)
        {
            var items = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries)
                .Where(x => x is not "")
                .Concat(new[] { "[[2]]", "[[6]]" })
                .Select(x => Packet.Parse(x))
                .OrderBy(x => x).ToArray();

            var sum = items
                .Select((x, i) => (x, Number: i + 1))
                .Where(x => x.x.ToString() is "[[2]]" or "[[6]]")
                .Aggregate(1, (mul, x) => mul * x.Number);

            return sum.ToString();
        }


        class Packet : IComparable<Packet>
        {
            readonly GroupElement _group;

            private Packet(GroupElement group) => _group = group;

            public static Packet Parse(string input)
            {
                var packet = new Packet((GroupElement)GroupElement.Parse(input).Element);
                return packet;
            }

            public int Compare(Packet another) => _group.Compare(another._group);
            public int CompareTo(Packet? other) => other!.Compare(this);

            public override string ToString() => _group.ToString();
        }

        abstract class Element
        {
            public static (Element Element, int EndPos) Parse(ReadOnlySpan<char> input)
            {
                var result = input[0] switch
                {
                    >= '0' and <= '9' => NumberElement.Parse(input),
                    '[' => GroupElement.Parse(input),
                    _ => throw new InvalidOperationException()
                };

                return result;
            }

            public abstract int Compare(Element element);
        }
        class NumberElement : Element
        {
            public int Value { get; }

            public NumberElement(int value) => Value = value;

            public static new (Element Element, int EndPos) Parse(ReadOnlySpan<char> input)
            {
                var pos = 0;
                while (pos < input.Length)
                {
                    if (char.IsNumber(input[pos]) is false)
                        break;

                    pos++;
                }

                return (new NumberElement(int.Parse(input.Slice(0, pos))), pos);
            }

            public override string ToString() => Value.ToString();

            public override int Compare(Element element)
            {
                if (element is GroupElement groupElement)
                    return new GroupElement(this).Compare(groupElement);

                if (element is NumberElement numberElement)
                {
                    if (Value < numberElement.Value)
                        return 1;
                    if (Value > numberElement.Value)
                        return -1;
                    return 0;
                }

                throw new InvalidOperationException();
            }
        }
        class GroupElement : Element
        {
            public Element[] Elements { get; }

            public GroupElement(Element[] elements) => Elements = elements;

            public GroupElement(Element element) => Elements = new[] { element };

            public static new (Element Element, int EndPos) Parse(ReadOnlySpan<char> input)
            {
                if (input[0] is not '[')
                    throw new InvalidOperationException();
                if (input[1] is ']')
                    return (new GroupElement(new Element[0]), 2);

                var elements = new List<Element>();
                var (startPos, endPos) = (1, 1);
                while (endPos < input.Length)
                {
                    if (input[startPos] is ',')
                    {
                        endPos++;
                        startPos = endPos;
                        continue;
                    }

                    if (input[startPos] is ']')
                    {
                        endPos++;
                        break;
                    }

                    (var element, endPos) = Element.Parse(input.Slice(startPos));
                    elements.Add(element);
                    endPos += startPos;
                    startPos = endPos;
                }

                return (new GroupElement(elements.ToArray()), endPos);
            }

            public override int Compare(Element another)
            {
                if (another is NumberElement numberElement)
                    return Compare(new GroupElement(numberElement));

                if (another is GroupElement groupElement)
                {
                    foreach (var (left, right) in Elements.Zip(groupElement.Elements))
                    {
                        var result = left.Compare(right);
                        if (result is not 0)
                            return result;
                    }

                    // 같을 경우 경우 원소 갯수로 판정한다.
                    if (Elements.Length < groupElement.Elements.Length)
                        return 1;
                    else if (Elements.Length > groupElement.Elements.Length)
                        return -1;

                    return 0;
                }

                throw new InvalidOperationException();
            }

            public sealed override string ToString() => $"[{string.Join(",", (IEnumerable<Element>)Elements)}]";
        }
    }
}
