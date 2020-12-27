using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day14
    {
        public static async Task SolvePart1()
        {
            var input = await GetInput();

            var unique = input
                .GroupBy(i => i.Address)
                .Select(group => group.Last())
                .ToList();

            var sum = unique
                .Select(x => ApplyMaskPart1(x.Value, x.Mask))
                .Aggregate((a, b) => a + b);

            Console.WriteLine(sum);
        }

        private static ulong ApplyMaskPart1(ulong value, BitMask[] mask)
        {
            var zeros = mask
                .Select((x, i) => (Bit: x, Index: i))
                .Where(x => x.Bit == BitMask.Zero)
                .Select(x => ~(1uL << x.Index))
                .DefaultIfEmpty(~0uL)
                .Aggregate((a, b) => a & b);

            var ones = mask
                .Select((x, i) => (Bit: x, Index: i))
                .Where(x => x.Bit == BitMask.One)
                .Select(x => 1uL << x.Index)
                .DefaultIfEmpty(0uL)
                .Aggregate((a, b) => a | b);

            return (value & zeros) | ones;
        }

        public static async Task SolvePart2()
        {
            var input = await GetInput();

            var sum = input
                .SelectMany(x => ApplyMaskPart2(x.Address, x.Mask).Select(y => (Address: y, x.Value)))
                .GroupBy(x => x.Address)
                .Select(x => x.Last().Value)
                .Aggregate((a, b) => a + b);

            Console.WriteLine(sum);
        }

        private static ulong[] ApplyMaskPart2(ulong value, BitMask[] maskBase)
        {
            var masks = ExplodeMaskPart2(maskBase);

            var list = new List<ulong>();

            foreach (var mask in masks)
            {
                var ones = mask
                    .Select((x, i) => (Bit: x, Index: i))
                    .Where(x => x.Bit == BitMask.One)
                    .Select(x => 1uL << x.Index)
                    .DefaultIfEmpty(0uL)
                    .Aggregate((a, b) => a | b);

                list.Add(value | ones);
            }

            return list.ToArray();
        }

        private static IEnumerable<BitMask[]> ExplodeMaskPart2(BitMask[] mask)
        {
            var index = -1;
            for (var i = 0; i < mask.Length; i++)
                if (mask[i] == BitMask.Any)
                {
                    index = i;
                    break;
                }

            if (index == -1)
            {
                return new[] { mask };
            }

            var copy1 = new BitMask[mask.Length];
            var copy2 = new BitMask[mask.Length];
            Array.Copy(mask, copy1, mask.Length);
            Array.Copy(mask, copy2, mask.Length);
            copy1[index] = BitMask.Zero;
            copy2[index] = BitMask.One;
            
            return new[] { copy1, copy2 }.SelectMany(ExplodeMaskPart2);
        }

        public static async Task<AddressInsert[]> GetInput()
        {
            var lines = await File.ReadAllLinesAsync(@"Day14\Day14Input.txt");

            var groups = Utils.GroupByLines(lines, line => line.StartsWith("mask")).Skip(1).ToList();

            var regex = new Regex(@"mem\[(\d+)\] = (\d+)");

            var list = new List<AddressInsert>();

            foreach(var group in groups)
            {
                var mask = group[0]
                    .Substring(7)
                    .Select(c => c switch
                    {
                        '0' => BitMask.Zero,
                        '1' => BitMask.One,
                        _ => BitMask.Any
                    })
                    .Reverse()
                    .ToArray();

                var inserts = group
                    .Skip(1)
                    .Select(line => regex.Match(line))
                    .Select(match => new AddressInsert(ulong.Parse(match.Groups[1].Value), ulong.Parse(match.Groups[2].Value), mask));

                list.AddRange(inserts);
            }

            return list.ToArray();
        }

        public record AddressInsert(ulong Address, ulong Value, BitMask[] Mask);

        public enum BitMask
        {
            Zero, One, Any
        }
    }
}
