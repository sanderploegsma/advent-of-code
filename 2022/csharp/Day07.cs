using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022;

internal class Day07
{
    public interface INode
    {
        string Name { get; }
        int Size { get; }
    }
    
    public class Directory : INode
    {
        public string Name { get; init; }
        public Directory? Parent { get; init; }
        public List<INode> Children { get; } = new List<INode>();
        public int Size => Children.Sum(x => x.Size);
    }

    public class File : INode
    {
        public string Name { get; init; }
        public int Size { get; init; }
    }

    private readonly Directory _root;

    public Day07(IEnumerable<string> input)
    {
        Directory current = null;

        foreach (var line in input)
        {
            if (line == "$ cd /")
            {
                current = new Directory { Name = "/" };
                _root = current;
                continue;
            }
            if (line == "$ cd ..")
            {
                current = current.Parent;
                continue;
            }
            if (line.StartsWith("$ cd"))
            {
                var name = line.Substring(5);
                if (current.Children.FirstOrDefault(x => x.Name == name) is Directory existing)
                {
                    current = existing;
                    continue;
                }
                else
                {
                    var d = new Directory { Name = name, Parent = current };
                    current.Children.Add(d);
                    current = d;
                }
            }
            if (line == "$ ls")
            {
                continue;
            }
            if (line.StartsWith("dir"))
            {
                var dirName = line.Substring(4);
                if (current.Children.All(x => x.Name != dirName))
                {
                    var d = new Directory { Name = dirName, Parent = current };
                    current.Children.Add(d);
                    continue;
                }
            }

            var size = line.Split(' ')[0];
            var fileName = line.Split(' ')[1];

            if (current.Children.All(x => x.Name != fileName))
            {
                var f = new File { Name = fileName, Size = int.Parse(size) };
                current.Children.Add(f);
            }
        }
    }
    
    public int PartOne()
    {
        var sum = 0;
        var queue = new Queue<Directory>();
        queue.Enqueue(_root);

        while (queue.Any())
        {
            var dir = queue.Dequeue();
            if (dir.Size <= 100000)
            {
                sum += dir.Size;
            }
            foreach (var c in dir.Children)
            {
                if (c is Directory d)
                {
                    queue.Enqueue(d);
                }
            }
        }

        return sum;
    }

    public int PartTwo()
    {
        var freeSpace = 70000000 - _root.Size;
        var requiredSpace = 30000000 - freeSpace;

        var candidates = new List<Directory>();
        var queue = new Queue<Directory>();
        queue.Enqueue(_root);

        while (queue.Any())
        {
            var dir = queue.Dequeue();
            if (dir.Size >= requiredSpace)
            {
                candidates.Add(dir);
            }
            foreach (var c in dir.Children)
            {
                if (c is Directory d)
                {
                    queue.Enqueue(d);
                }
            }
        }

        return candidates.Select(d => d.Size).Min();
    }
}

public class Day07Test
{
    private const string InputFile = "Day07.Input.txt";
    private const string ExampleFile = "Day07.Example.txt";

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 95437)]
    [FileData(InputFile, FileContents.StringPerLine, 1477771)]
    public void TestPartOne(IEnumerable<string> input, int expected)
    {
        var solution = new Day07(input);
        Assert.Equal(expected, solution.PartOne());
    }

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 24933642)]
    [FileData(InputFile, FileContents.StringPerLine, 3579501)]
    public void TestPartTwo(IEnumerable<string> input, int expected)
    {
        var solution = new Day07(input);
        Assert.Equal(expected, solution.PartTwo());
    }
}
