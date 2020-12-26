using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day13
    {
        public static async Task SolvePart1()
        {
            var lines = await GetInput();

            var time = int.Parse(lines[0]);
            var ids = lines[1]
                .Split(',')
                .Where(x => x != "x")
                .Select(int.Parse)
                .ToArray();

            var min = ids.Select(id => (Id: id, Wait: id - time % id)).MinBy(x => x.Wait);

            Console.WriteLine(min.Id * min.Wait);
        }

        public static async Task SolvePart2()
        {
            var lines = await GetInput();

            var split = lines[1].Split(',');
            var timestamp = int.Parse(split[0]);
            var buses = split
                .Select((x, i) => x == "x" ? null : new { Id = int.Parse(x), Offset = i })
                .Where(x => x != null)
                .Select(x => new { x.Id, x.Offset, Delay = x.Id - (timestamp % x.Id) })
                //.OrderBy(x => x.Delay)
                .ToArray();

            Func<int, int, int> absoluteModulo = (a, b) => ((a % b) + b) % b;
            Func<long, long, long> inverse = (a, mod) =>
            {
                var b = a % mod;
                for (var i = 1; i < mod; i++)
                {
                    if ((b * i) % mod == 1)
                    {
                        return i;
                    }
                }
                return 1;
            };

            var n = buses.Aggregate(1L, (a, b) => a * b.Id);
            var sum = buses.Aggregate(new BigInteger(), (a, b) =>
            {
                var x = absoluteModulo(b.Id - b.Offset, b.Id);
                var nU = n / b.Id;
                var inversed = inverse(nU, b.Id);
                Console.WriteLine($"x = {x} (mod {b.Id})");
                return a + (x * nU * inversed);
            });

            var chineseRemainder = sum % n;

            Console.WriteLine(chineseRemainder);
        }

        public static async Task<string[]> GetInput()
        {
            return await File.ReadAllLinesAsync(@"Day13\Day13Input.txt");
        }
    }
}
