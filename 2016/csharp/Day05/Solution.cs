using System.Linq;
using System.Security.Cryptography;

namespace AdventOfCode2016.Day05
{
    internal class Solution
    {
        private const int PasswordLength = 8;
        
        private readonly string _input;

        public Solution(string input)
        {
            _input = input;
        }

        public string PartOne()
        {
            using var md5 = MD5.Create();
            var i = 0;
            var password = "";

            while (password.Length < PasswordLength)
            {
                var hash = $"{_input}{i}".Hash(md5);

                if (hash.StartsWith("00000"))
                {
                    password += hash[5];
                }
                
                i++;
            }

            return password.ToLower();
        }

        public string PartTwo()
        {
            using var md5 = MD5.Create();
            var i = 0;
            var password = Enumerable.Repeat('_', PasswordLength).ToArray();

            while (password.Contains('_'))
            {
                var hash = $"{_input}{i}".Hash(md5);

                if (hash.StartsWith("00000"))
                {
                    var position = (int) char.GetNumericValue(hash[5]);

                    if (position >= 0 && position < PasswordLength && password[position] == '_')
                    {
                        password[position] = hash[6];
                    }
                }
                
                i++;
            }

            return string.Concat(password).ToLower();
        }
    }
}