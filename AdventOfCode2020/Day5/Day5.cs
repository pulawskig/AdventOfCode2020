using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day5
    {
        public static async Task SolvePart1()
        {
            var input = await GetInput();

            var ids = GetIds(input);

            Console.WriteLine(ids.Max());
        }

        public static async Task SolvePart2()
        {
            var input = await GetInput();

            var ids = GetIds(input);

            var max = ids.Max();

            var except = Enumerable.Range(70, max - 70).Except(ids);

            foreach (var e in except)
                Console.WriteLine(e);
        }

        private static async Task<(bool[], bool[])[]> GetInput()
        {
            var lines = await File.ReadAllLinesAsync(@"Day5\Day5Input.txt");

            return lines.Select(line => (line.Take(7).Select(c => c == 'B').ToArray(), line.Skip(7).Select(c => c == 'R').ToArray())).ToArray();
        }

        private static IEnumerable<int> SplitBinarily(IEnumerable<int> list, bool isUpper)
        {
            var half = (int) Math.Ceiling(list.Count() / 2f);
            return isUpper ? list.Skip(half) : list.Take(half);
        }

        private static List<int> GetIds((bool[], bool[])[] input)
        {
            var ids = new List<int>();
            foreach (var (rowMoves, columnMoves) in input)
            {
                var rows = Enumerable.Range(0, 128);
                foreach (var move in rowMoves)
                    rows = SplitBinarily(rows, move);

                var columns = Enumerable.Range(0, 8);
                foreach (var move in columnMoves)
                    columns = SplitBinarily(columns, move);

                var row = rows.Single();
                var column = columns.Single();

                ids.Add(row * 8 + column);
            }
            return ids;
        }
    }
}
