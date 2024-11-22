using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day04
{
    internal static class PassportParser
    {
        private const string FieldBirthYear = "byr";
        private const string FieldIssueYear = "iyr";
        private const string FieldExpirationYear = "eyr";
        private const string FieldHeight = "hgt";
        private const string FieldHairColor = "hcl";
        private const string FieldEyeColor = "ecl";
        private const string FieldPassportId = "pid";
        private const string FieldCountryId = "cid";

        private static readonly string[] RequiredFields =
        {
            FieldBirthYear, FieldIssueYear, FieldExpirationYear, FieldHeight, FieldHairColor, FieldEyeColor,
            FieldPassportId
        };

        public static Passport? Parse(string passport)
        {
            var fields = passport
                .Split(new[] {Environment.NewLine, " "}, StringSplitOptions.RemoveEmptyEntries)
                .Select(field => field.Split(":"))
                .ToDictionary(field => field[0], field => field[1]);

            if (RequiredFields.Any(field => !fields.ContainsKey(field)))
                return null;

            return new Passport(
                birthYear: fields[FieldBirthYear],
                issueYear: fields[FieldIssueYear],
                expirationYear: fields[FieldExpirationYear],
                height: fields[FieldHeight],
                hairColor: fields[FieldHairColor],
                eyeColor: fields[FieldEyeColor],
                passportId: fields[FieldPassportId],
                countryId: fields.GetValueOrDefault(FieldCountryId, null)
            );
        }
    }
}
