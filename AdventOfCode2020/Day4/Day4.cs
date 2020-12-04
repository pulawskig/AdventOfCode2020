using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day4
    {
        public static async Task SolvePart1()
        {
            var input = await GetInput();

            var checks = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" }.Select(check => new Regex($"{check}:")).ToArray();

            var count = 0;

            foreach(var line in input)
            {
                if (checks.All(check => check.IsMatch(line)))
                    count++;
            }

            Console.WriteLine(count);
        }

        public static async Task SolvePart2()
        {
            var input = await GetInput();

            var checks = new[]
            {
                @"byr:(19[2-9][0-9]|200[1-2])",
                @"iyr:20(1[0-9]|20)",
                @"eyr:20(2[0-9]|30)",
                @"hgt:(1([5-8][0-9]|9[0-3])cm|(59|(6[0-9]|7[0-6]))in)",
                @"hcl:#[0-9a-f]{6}",
                @"ecl:(amb|(blu|(brn|(gry|(grn|(hzl|oth))))))",
                @"pid:\d{9}"
            }
            .Select(check => new Regex(check))
            .ToArray();

            var count = 0;

            foreach (var line in input)
            {
                if (checks.All(check => check.IsMatch(line)))
                    count++;
            }

            Console.WriteLine(count);
        }

        private static async Task<IEnumerable<string>> GetInput()
        {
            var lines = await File.ReadAllLinesAsync(@"Day4\Day4Input.txt");
            var together = string.Join(' ', lines.Select(l => string.IsNullOrWhiteSpace(l) ? "\t" : l));
            var input = together.Split('\t').Select(l => l.Trim(' '));
            return input.ToList();
        }
    }
}
