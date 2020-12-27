using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day16
    {
        public static async Task SolvePart1()
        {
            var (fields, tickets) = await GetInput();

            var sum = 0;
            foreach(var ticket in tickets.Skip(1))
            {
                sum += ticket
                    .Where(value => !fields.Any(field => (value >= field.Range1.Start && value <= field.Range1.End) || (value >= field.Range2.Start && value <= field.Range2.End)))
                    .Sum();
            }

            Console.WriteLine(sum);
        }


        public static async Task SolvePart2()
        {
            var (fields, tickets) = await GetInput();

            tickets = tickets
                .Where(ticket => ticket.All(value => fields.Any(field => (value >= field.Range1.Start && value <= field.Range1.End) || (value >= field.Range2.Start && value <= field.Range2.End))))
                .ToList();

            var indexes = new Dictionary<Field, int>();
            foreach(var field in fields)
            {
                for(var i = 0; i < fields.Length; i++)
                {
                    var proper = tickets
                        .Select(ticket => ticket[i])
                        .All(value => (value >= field.Range1.Start && value <= field.Range1.End) || (value >= field.Range2.Start && value <= field.Range2.End));
                    
                    if(proper)
                    {
                        indexes.Add(field, i);
                        break;
                    }
                }
            }

            var sum = indexes
                .Where(x => x.Key.Name.StartsWith("departure"));
                var sum1 = sum
                .Select(x => x.Value);
            var sum2 = sum1
                .Select(x => tickets[0][x]);
            var sum3 = sum2
                .Aggregate((a, b) => a * b);

            Console.WriteLine(sum3);
        }

        public static async Task<(Field[] Fields, List<int[]> Tickets)> GetInput()
        {
            var lines = await File.ReadAllLinesAsync(@"Day16\Day16Input.txt");

            var groups = Utils.GroupByEmptyLines(lines);

            var fieldRegex = new Regex(@"([a-z ]+): (\d+)-(\d+) or (\d+)-(\d+)");
            var fields = groups[0]
                .Skip(1)
                .Select(line => fieldRegex.Match(line).Groups)
                .Select(g => new Field(g[1].Value, new Range(int.Parse(g[2].Value), int.Parse(g[3].Value)), new Range(int.Parse(g[4].Value), int.Parse(g[5].Value))))
                .ToArray();

            var tickets = groups[1]
                .Skip(1)
                .Concat(groups[2].Skip(1))
                .Select(line => line
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray())
                .ToList();

            return (fields, tickets);
        }

        public record Field(string Name, Range Range1, Range Range2);
        public record Range(int Start, int End);
    }
}
