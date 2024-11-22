using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2016.Day09
{
    internal class Solution
    {
        private readonly string _input;

        public Solution(string input)
        {
            _input = input;
        }

        public long PartOne()
        {
            var result = "";
            var i = 0;

            while (i < _input.Length)
            {
                if (_input[i] == '(')
                {
                    var j = _input.IndexOf(')', i);
                    var marker = _input[i..j];
                    var (count, repeat) = ParseMarker(marker);
                    var sequence = _input.Substring(j + 1, count);

                    result += string.Concat(Enumerable.Repeat(sequence, repeat));
                    i = j + count + 1;
                }
                else
                {
                    result += _input[i];
                    i++;
                }
            }

            return result.LongCount();
        }

        public long PartTwo() => GetLength(_input);

        private static long GetLength(string compressed)
        {
            var length = 0L;
            var i = 0;

            while (i < compressed.Length)
            {
                if (compressed[i] == '(')
                {
                    var j = compressed.IndexOf(')', i);
                    var marker = compressed[i..j];
                    var (count, repeat) = ParseMarker(marker);
                    var sequenceLength = GetLength(compressed.Substring(j + 1, count));

                    length += sequenceLength * repeat;
                    i = j + count + 1;
                }
                else
                {
                    i++;
                    length++;
                }
            }

            return length;
        }

        private static (int Count, int Repeat) ParseMarker(string marker)
        {
            var segments = marker.Trim('(', ')').Split('x').Select(int.Parse).ToArray();
            return (segments[0], segments[1]);
        }
    }
}
