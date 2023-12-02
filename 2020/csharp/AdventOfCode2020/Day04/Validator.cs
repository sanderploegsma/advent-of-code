using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day04
{
    internal static class PassportValidator
    {
        private const int MinimumBirthYear = 1920;
        private const int MaximumBirthYear = 2002;
        
        private const int MinimumIssueYear = 2010;
        private const int MaximumIssueYear = 2020;
        
        private const int MinimumExpirationYear = 2020;
        private const int MaximumExpirationYear = 2030;

        private const string HairColorPattern = @"^#[\da-f]{6}$";
        private const string PassportIdPattern = @"^\d{9}$";

        private static readonly ISet<string> EyeColors = new HashSet<string> {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

        private static readonly IPassportRule[] PassportRules =
        {
            new FieldRule(x => x.BirthYear, Rules.IsNumberInRange(MinimumBirthYear, MaximumBirthYear)),
            new FieldRule(x => x.IssueYear, Rules.IsNumberInRange(MinimumIssueYear, MaximumIssueYear)),
            new FieldRule(x => x.ExpirationYear, Rules.IsNumberInRange(MinimumExpirationYear, MaximumExpirationYear)),
            new FieldRule(x => x.HairColor, Rules.MatchesPattern(HairColorPattern)),
            new FieldRule(x => x.EyeColor, Rules.IsElementOf(EyeColors)),
            new FieldRule(x => x.PassportId, Rules.MatchesPattern(PassportIdPattern)),
            new IsValidHeight()
        };

        public static bool IsValid(Passport passport) => PassportRules.All(rule => rule.IsValid(passport));
    }
    
    internal interface IPassportRule
    {
        bool IsValid(Passport passport);
    }

    internal class FieldRule : IPassportRule
    {
        private readonly Func<Passport, string> _fieldSelector;
        private readonly Predicate<string> _predicate;

        public FieldRule(Func<Passport, string> fieldSelector, Predicate<string> predicate)
        {
            _fieldSelector = fieldSelector;
            _predicate = predicate;
        }
        
        public bool IsValid(Passport passport) => _predicate.Invoke(_fieldSelector.Invoke(passport));
    }

    internal class IsValidHeight : IPassportRule
    {
        private const string HeightUnitCentimeters = "cm";
        private const int MinimumHeightInCentimeters = 150;
        private const int MaximumHeightInCentimeters = 193;
        
        private const string HeightUnitInches = "in";
        private const int MinimumHeightInInches = 59;
        private const int MaximumHeightInInches = 76;
        
        private static readonly string HeightPattern = $"^(?<value>\\d+)(?<unit>{HeightUnitCentimeters}|{HeightUnitInches})$";

        public bool IsValid(Passport passport)
        {
            var match = Regex.Match(passport.Height, HeightPattern);

            if (!match.Success)
                return false;

            var unit = match.Groups["unit"].Value;
            var height = match.Groups["value"].Value;

            return unit == HeightUnitCentimeters && IsValidCentimeters(height) ||
                   unit == HeightUnitInches && IsValidInches(height);
        }

        private static Predicate<string> IsValidCentimeters =>
            Rules.IsNumberInRange(MinimumHeightInCentimeters, MaximumHeightInCentimeters);
        
        private static Predicate<string> IsValidInches => 
            Rules.IsNumberInRange(MinimumHeightInInches, MaximumHeightInInches);
    }

    internal static class Rules
    {
        public static Predicate<string> IsNumberInRange(int min, int max) => 
            number => int.TryParse(number, out var value) && value >= min && value <= max;

        public static Predicate<string> MatchesPattern(string pattern) =>
            value => Regex.IsMatch(value, pattern);

        public static Predicate<string> IsElementOf(ISet<string> set) => set.Contains;
    }
}