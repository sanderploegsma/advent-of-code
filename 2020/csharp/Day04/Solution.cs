using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day04
{
    internal class Solution
    {
        private readonly IReadOnlyCollection<Passport> _passports;

        public Solution(string input)
        {
            _passports = input
                .Split(Environment.NewLine + Environment.NewLine)
                .Select(PassportParser.Parse)
                .WhereNotNull()
                .ToList();
        }

        public int PartOne() => _passports.Count;

        public int PartTwo() => _passports.Count(PassportValidator.IsValid);
    }

    internal class Passport
    {
        public string BirthYear { get; }
        public string IssueYear { get; }
        public string ExpirationYear { get; }
        public string Height { get; }
        public string HairColor { get; }
        public string EyeColor { get; }
        public string PassportId { get; }
        public string? CountryId { get; }

        public Passport(string birthYear, string issueYear, string expirationYear, string height, string hairColor,
            string eyeColor, string passportId, string? countryId)
        {
            BirthYear = birthYear;
            IssueYear = issueYear;
            ExpirationYear = expirationYear;
            Height = height;
            HairColor = hairColor;
            EyeColor = eyeColor;
            PassportId = passportId;
            CountryId = countryId;
        }
    }
}