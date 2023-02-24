namespace CiT.CLI.Info;

public static class DomainBlocks
{
    public const string
        Main = """
Shows information about domain blocks.

Usage:
	cit domain-blocks <command> [<Arg>...]

Available commands:

show
	show all domain blocks
query <domain>
	get information about a specific domain
add <domain> [<severity> <comment>]
	add a domain to the blocklist
remove <domain>
	remove a domain from the blocklist
<command> help
	show more information about a specific command
""",
        Show = """
Lists all domain blocks, their severity, and any comments.

Usage:
	cit domain-blocks show
""",
        Query = """
Shows information about a specific domain.

Usage:
	cit domain-blocks query <domain>

Where:
	<domain> is a domain on the blocklist.
""",
        Add = """
Adds a domain to the blocklist, optionally specifying the severity and a comment.

Usage:
	cit domain-blocks add <domain> [<severity> <private comment>]

Where:
	<domain> is the domain to add
	<severity> is the severity of the block, should be one of
	  - "silence", or
	  - "suspend"
	<comment> is a private comment to be added to the block
""",
        Remove = """
Removes a domain from the blocklist.

Usage:
	cit domain-blocks remove <domain>

Where:
	<domain> is the domain to remove.
""";
}
