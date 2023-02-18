using System;
using System.IO;

namespace CiT.CLI.Tests.Helpers;

public class ConsoleOutput : IDisposable
{
    private readonly StringWriter _stringWriter;
    private readonly TextWriter _originalOutput;
    public ConsoleOutput()
    {
        _stringWriter = new StringWriter();
        _originalOutput = Console.Out;
        Console.SetOut(_stringWriter);
    }
    public string GetOutput(bool trimEnd = true)
        => trimEnd ? _stringWriter.ToString().TrimEnd('\r', '\n') : _stringWriter.ToString();
    /// <inheritdoc />
    public void Dispose()
    {
        Console.SetOut(_originalOutput);
        _stringWriter.Dispose();
    }
}
