using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day12
    {
        public static async Task SolvePart1()
        {
            var input = await GetInput();

            var direction = Direction.East;
            var north = 0;
            var east = 0;

            foreach(var command in input)
            {
                switch (command.Type)
                {
                    case Command.CommandType.Direction:
                        MoveDirectionPart1(command.Direction.Value, command.Value, ref north, ref east);
                        break;
                    case Command.CommandType.Forward:
                        MoveDirectionPart1(direction, command.Value, ref north, ref east);
                        break;
                    case Command.CommandType.Turn:
                        direction = command.IsRight.Value
                            ? (Direction)(((int)direction + (command.Value / 90)) % 4)
                            : (Direction)((4 + (int)direction - (command.Value / 90)) % 4);
                        break;
                }
            }

            Console.WriteLine(Math.Abs(north) + Math.Abs(east));
        }

        private static void MoveDirectionPart1(Direction direction, int amount, ref int north, ref int east)
        {
            north = direction switch
            {
                Direction.North => north + amount,
                Direction.South => north - amount,
                _ => north
            };
            east = direction switch
            {
                Direction.East => east + amount,
                Direction.West => east - amount,
                _ => east
            };
        }

        public static async Task SolvePart2()
        {
            var input = await GetInput();

            var north = 0;
            var east = 0;

            var waypointDirection = Direction.East;
            var waypointNorth = 1;
            var waypointEast = 10;

            foreach (var command in input)
            {
                switch (command.Type)
                {
                    case Command.CommandType.Direction:
                        MoveDirectionPart1(command.Direction.Value, command.Value, ref waypointNorth, ref waypointEast);
                        break;
                    case Command.CommandType.Forward:
                        north += waypointNorth * command.Value;
                        east += waypointEast * command.Value;
                        break;
                    case Command.CommandType.Turn:
                        MoveTurnPart2(command.IsRight.Value, command.Value, ref waypointDirection, ref waypointNorth, ref waypointEast);
                        break;
                }
            }

            Console.WriteLine(Math.Abs(north) + Math.Abs(east));
        }

        private static void MoveTurnPart2(bool isRight, int value, ref Direction currentDirection, ref int currentNorth, ref int currentEast)
        {
            var tempDirection = currentDirection;

            currentDirection = isRight
                            ? (Direction)(((int)currentDirection + (value / 90)) % 4)
                            : (Direction)((4 + (int)currentDirection - (value / 90)) % 4);

            if(value == 180 || value == 0)
            {
                currentNorth = -currentNorth;
                currentEast = -currentEast;
            }
            else if((isRight && value == 90) || (!isRight && value == 270))
            {
                var tempNorth = currentNorth;
                currentNorth = -currentEast;
                currentEast = tempNorth;
            }
            else if((isRight && value == 270) || (!isRight && value == 90))
            {
                var tempNorth = currentNorth;
                currentNorth = currentEast;
                currentEast = -tempNorth;
            }
        }

        private static async Task<Command[]> GetInput()
        {
            var lines = await File.ReadAllLinesAsync(@"Day12\Day12Input.txt");

            return lines
                .Select(line => (C: line.First(), V: int.Parse(line[1..])))
                .Select(x => new Command
                {
                    Value = x.V,
                    Type = x.C switch
                    {
                        'F' => Command.CommandType.Forward,
                        'R' or 'L' => Command.CommandType.Turn,
                        _ => Command.CommandType.Direction
                    },
                    Direction = x.C switch
                    {
                        'N' => Direction.North,
                        'E' => Direction.East,
                        'S' => Direction.South,
                        'W' => Direction.West,
                        _ => null
                    },
                    IsRight = x.C switch
                    {
                        'R' => true,
                        'L' => false,
                        _ => null
                    }
                })
                .ToArray();
        }

        private enum Direction
        {
            North, East, South, West
        }

        private class Command
        {
            public enum CommandType
            {
                Direction, Turn, Forward
            }

            public CommandType Type { get; set; }

            public int Value { get; set; }

            public Direction? Direction { get; set; }

            public bool? IsRight { get; set; }        }
    }
}
