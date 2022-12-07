using System.Text.RegularExpressions;

namespace AdventOfCode2022;

internal partial class Day07
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

    private readonly Directory _root = new() { Name = "/" };

    public Day07(IEnumerable<string> input)
    {
        var current = _root;

        foreach (var line in input)
        {
            switch (line)
            {
                case "$ cd /":
                    current = _root;
                    break;
                case "$ cd ..":
                    if (current.Parent != null)
                    {
                        current = current.Parent;
                    }
                    break;
                case string cd when cd.StartsWith("$ cd"):
                    var targetName = cd[5..];
                    if (current.Children.FirstOrDefault(x => x.Name == targetName) is Directory target)
                    {
                        current = target;
                    }
                    break;
                case string dir when dir.StartsWith("dir "):
                    var dirName = dir[4..];
                    if (current.Children.All(x => x.Name != dirName))
                    {
                        current.Children.Add(new Directory { Name = dirName, Parent = current });
                    }
                    break;
                case string file when FilePattern().IsMatch(file):
                    var fileSize = file.Split(' ')[0];
                    var fileName = file.Split(' ')[1];
                    if (current.Children.All(x => x.Name != fileName))
                    {
                        current.Children.Add(new File { Name = fileName, Size = int.Parse(fileSize) });
                    }
                    break;
            }
        }
    }

    public int PartOne()
    {
        return Traverse(dir => dir.Size <= 100000).Sum(d => d.Size);
    }

    public int PartTwo()
    {
        var freeSpace = 70000000 - _root.Size;
        var requiredSpace = 30000000 - freeSpace;

        return Traverse(dir => dir.Size >= requiredSpace).Select(d => d.Size).Min();
    }

    private IEnumerable<Directory> Traverse(Func<Directory, bool> predicate)
    {
        var queue = new Queue<Directory>();
        queue.Enqueue(_root);

        while (queue.Any())
        {
            var dir = queue.Dequeue();
            if (predicate.Invoke(dir))
            {
                yield return dir;
            }
            foreach (var c in dir.Children)
            {
                if (c is Directory d)
                {
                    queue.Enqueue(d);
                }
            }
        }
    }

    [GeneratedRegex("\\d+ .*")]
    private static partial Regex FilePattern();
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
