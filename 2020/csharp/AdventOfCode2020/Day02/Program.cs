using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day02
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var input = File.ReadLines("Input.txt").ToList();

            Console.WriteLine("Part one: {0}", PartOne(input));
            Console.WriteLine("Part two: {0}", PartTwo(input));
        }

        private static int PartOne(IEnumerable<string> input) => 
            input.Select(Parser.ParseCharacterCountBasedPolicy).Count(x => x.IsValid);

        private static int PartTwo(IEnumerable<string> input) => 
            input.Select(Parser.ParseCharacterPositionBasedPolicy).Count(x => x.IsValid);
    }

    internal static class Parser
    {
        private const string Pattern = @"^(?<a>\d+)-(?<b>\d+) (?<char>[a-z]): (?<password>[a-z]+)$";

        public static PasswordWithPolicy ParseCharacterCountBasedPolicy(string line)
        {
            var match = Regex.Match(line, Pattern);
            
            return new PasswordWithPolicy
            {
                Password = match.Groups["password"].Value,
                Policy = new CharacterCountBasedPolicy
                {
                    Character = match.Groups["char"].Value[0],
                    MinCount = int.Parse(match.Groups["a"].Value),
                    MaxCount = int.Parse(match.Groups["b"].Value)
                }
            };
        }

        public static PasswordWithPolicy ParseCharacterPositionBasedPolicy(string line)
        {
            var match = Regex.Match(line, Pattern);
            
            return new PasswordWithPolicy
            {
                Password = match.Groups["password"].Value,
                Policy = new CharacterPositionBasedPolicy
                {
                    Character = match.Groups["char"].Value[0],
                    FirstPosition = int.Parse(match.Groups["a"].Value),
                    SecondPosition = int.Parse(match.Groups["b"].Value)
                }
            };
        }
    }

    internal class PasswordWithPolicy
    {
        public string Password { get; set; }
        public IPasswordPolicy Policy { get; set; }

        public bool IsValid => Policy.IsValid(Password);
    }

    internal interface IPasswordPolicy
    {
        bool IsValid(string password);
    }

    internal class CharacterCountBasedPolicy : IPasswordPolicy
    {
        public char Character { get; set; }
        public int MinCount { get; set; }
        public int MaxCount { get; set; }

        public bool IsValid(string password)
        {
            var count = password.Count(c => c == Character);
            return count >= MinCount && count <= MaxCount;
        }
    }

    internal class CharacterPositionBasedPolicy : IPasswordPolicy
    {
        public char Character { get; set; }
        public int FirstPosition { get; set; }
        public int SecondPosition { get; set; }

        public bool IsValid(string password) =>
            password[FirstPosition - 1] == Character ^ password[SecondPosition - 1] == Character;
    }
}