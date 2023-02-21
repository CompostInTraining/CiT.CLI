namespace CiT.CLI.Info;

public class Domains
{
    public const string Main = """
Shows information about domains.

Usage:
    cit domains query <command>

Available commands:

query <domain>
    get information about a domain, including number of users, amount of media,
    etc.
<command> help
	show more information about a specific command
""",
        Query = """
Shows information about a provided domain.

Usage:
    cit domains query <command>

Where:
    <domain> is a domain your instance knows about.
""";
}
