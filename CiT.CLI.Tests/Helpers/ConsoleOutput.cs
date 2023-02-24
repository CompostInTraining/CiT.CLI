using System;
using System.IO;

namespace CiT.CLI.Tests.Helpers;

public class ConsoleOutput : IDisposable
{
    private readonly TextWriter _originalOutput;
    private readonly StringWriter _stringWriter;
    public ConsoleOutput()
    {
        _stringWriter = new StringWriter();
        _originalOutput = Console.Out;
        Console.SetOut(_stringWriter);
    }
    /// <inheritdoc />
    public void Dispose()
    {
        Console.SetOut(_originalOutput);
        _stringWriter.Dispose();
    }
    public string GetOutput(bool trimEnd = true)
        => trimEnd ? _stringWriter.ToString().TrimEnd('\r', '\n') : _stringWriter.ToString();
}
