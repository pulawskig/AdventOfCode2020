using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day15
    {
        public static async Task SolvePart1()
        {
            var input = await GetInput();

            for(var i = input.Count; i < 2020; i++)
            {
                var last = input.Last();

                if(input.Count(x => x == last) > 1)
                {
                    var lastTwo = input
                        .Select((x, i) => (Value: x, Index: i))
                        .Where(x => x.Value == last)
                        .TakeLast(2)
                        .ToArray();

                    input.Add(lastTwo[1].Index - lastTwo[0].Index);
                }
                else
                {
                    input.Add(0);
                }
            }

            Console.WriteLine(input.Last());
        }


        public static async Task SolvePart2()
        {
            var input = await GetInput();

            int position = 0;
            int lastNumber = input.Last();
            var positions = new int[30000000];
            foreach (var number in input)
                positions[number] = ++position;

            while (position < 30000000)
            {
                int lastPosition = positions[lastNumber];
                int nextNumber = lastPosition != 0 ? position - lastPosition : 0;
                positions[lastNumber] = position++;
                lastNumber = nextNumber;
            }

            Console.WriteLine(lastNumber);
        }

        public static async Task<List<int>> GetInput()
        {
            return (await File.ReadAllTextAsync(@"Day15\Day15Input.txt"))
                .Split(',')
                .Select(int.Parse)
                .ToList();
        }
    }
}
