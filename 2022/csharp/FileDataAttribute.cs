using System.Reflection;
using Xunit.Sdk;

namespace AdventOfCode2022;

internal class FileDataAttribute : DataAttribute
{
    private readonly string _filePath;
    private readonly object[] _args;

    public FileDataAttribute(string filePath, params object[] args)
    {
        _filePath = filePath;
        _args = args;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        var contents = File.ReadAllText(_filePath);

        yield return _args.Prepend(contents).ToArray();
    }
}
