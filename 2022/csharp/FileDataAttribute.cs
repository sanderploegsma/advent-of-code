using System.Reflection;
using Xunit.Sdk;

namespace AdventOfCode2022;

internal enum FileContents
{
    SingleString,
    StringPerLine,
}

internal class FileDataAttribute : DataAttribute
{
    private readonly string _filePath;
    private readonly FileContents _contents;
    private readonly object[] _args;

    public FileDataAttribute(string filePath, params object[] args) : this(filePath, FileContents.SingleString, args)
    {
    }

    public FileDataAttribute(string filePath, FileContents contents, params object[] args)
    {
        _filePath = filePath;
        _contents = contents;
        _args = args;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        var data = ReadFile();

        yield return _args.Prepend(data).ToArray();
    }

    private object ReadFile() => _contents switch
    {
        FileContents.SingleString => File.ReadAllText(_filePath),
        FileContents.StringPerLine => File.ReadLines(_filePath),
        _ => throw new ArgumentException(),
    };
}
