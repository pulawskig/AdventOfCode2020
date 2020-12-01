using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day1
    {
        public static async Task SolvePart1()
        {
            var inputs = await File.ReadAllLinesAsync(@"Day1\Day1Input.txt");

            var queue = new Queue<int>(inputs.Select(int.Parse));

            var first = 0;
            while(queue.Count > 0)
            {
                var item = queue.Dequeue();
                if(queue.Contains(2020 - item))
                {
                    first = item;
                    break;
                }
            }

            Console.WriteLine(first * (2020 - first));
        }

        public static async Task SolvePart2()
        {
            var inputs = await File.ReadAllLinesAsync(@"Day1\Day1Input.txt");

            var firstQueue = new Queue<int>(inputs.Select(int.Parse));
            var result1 = 0;
            var result2 = 0;
            while(firstQueue.Count > 0)
            {
                var first = firstQueue.Dequeue();

                var secondQueue = new Queue<int>(firstQueue);
                var second = -1;
                while(secondQueue.Count > 0)
                {
                    var candidate = secondQueue.Dequeue();
                    if(secondQueue.Contains(2020 - (first + candidate)))
                    {
                        second = candidate;
                        break;
                    }
                }

                if (second > 0)
                {
                    result1 = first;
                    result2 = second;
                    break;
                }
            }

            Console.WriteLine(result1 * result2 * (2020 - (result1 + result2)));
        }
    }
}
