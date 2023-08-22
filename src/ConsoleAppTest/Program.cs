using System.CommandLine;
using ConsoleAppTest.Commands;

namespace ConsoleAppTest;

class Program
{
    async static Task Main(string[] args)
    {
        var rootCommand = new RootCommand("Sample command-line app");

        rootCommand.SetHandler(() => { Console.WriteLine("Hello, World!"); });
        var subCommand1 = SubCommand1.BuildCommand();
        rootCommand.Add(subCommand1);
        await rootCommand.InvokeAsync(args);
    }
}