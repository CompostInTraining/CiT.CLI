namespace CiT.CLI.Info;

public static class Program
{
    public const string
        Main = """
Allows you to essentially moderate a Mastodon instance from your command line.

Usage:
	cit <command> <subcommand> [<Arg>...]

Available Commands:

domain-blocks
	cit domain-blocks <command> [<Arg>...]
email-domain-blocks
	cit email-domain-blocks <command> [Arg>...]
ip-blocks
	cit ip-blocks <command> [<Arg>...]

Use `cit <command> help` to show more information about a command.
""";
}