using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day8
    {
        public static async Task SolvePart1()
        {
            var input = await GetInput();

            var (accumulator, _) = Interpret(input);

            Console.WriteLine(accumulator);
        }

        public static async Task SolvePart2()
        {
            var input = await GetInput();

            for(int i = 0; i < input.Length; i++)
            {
                var current = input[i];
                var copy = current;

                if (current.Operation == "acc")
                    continue;

                if (current.Operation == "nop")
                    current.Operation = "jmp";
                else
                    current.Operation = "nop";

                input[i] = current;

                var (accumulator, loop) = Interpret(input);

                if(!loop)
                {
                    Console.WriteLine(accumulator);
                    break;
                }

                input[i] = copy;
            }
        }

        private static async Task<(string Operation, int Argument)[]> GetInput()
        {
            var lines = await File.ReadAllLinesAsync(@"Day8\Day8Input.txt");

            return lines
                .Select(line => line.Split(' '))
                .Select(split => (split[0], int.Parse(split[1])))
                .ToArray();
        }

        private static (int Accumulator, bool Loop) Interpret((string Operation, int Argument)[] input)
        {
            var visited = new bool[input.Length];

            var instruction = 0;
            var accumulator = 0;
            var loop = true;

            while (true)
            {
                if (instruction >= input.Length)
                {
                    loop = false;
                    break;
                }
                if (visited[instruction])
                    break;

                visited[instruction] = true;

                var (operation, argument) = input[instruction];

                switch (operation)
                {
                    case "nop":
                        instruction++;
                        break;
                    case "acc":
                        accumulator += argument;
                        instruction++;
                        break;
                    case "jmp":
                        instruction += argument;
                        break;
                    default:
                        break;
                }
            }

            return (accumulator, loop);
        }
    }
}
