using System.CommandLine;

namespace ConsoleAppTest.Commands;

public class SubCommand1
{
    public static Command BuildCommand()
    {
        var domainArgument = new Argument<string?>(
            "domain",
            () => null,
            "Domain to block"
        );
        var command = new Command("SubCommand1", "A subcommand from a different module");
        command.SetHandler(() =>
        {
            Console.WriteLine("Hello from SubCommand1");
        });
        var addCommand = new Command("add", "Add domain block");
        addCommand.Add(domainArgument);
        addCommand.SetHandler((domainValue) =>
        {
            Console.WriteLine($"Blocking domain {domainValue}");
        },
            domainArgument);
        command.Add(addCommand);
        return command;
    }
}
