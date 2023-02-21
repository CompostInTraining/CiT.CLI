namespace CiT.CLI.Info;

public static class EmailDomainBlocks
{
	public const string
		Main = """
Shows information about email domain blocks.

Usage:
	cit email-domain-blocks <command> [<Arg>...]

Available Commands:

show
	show all email domain blocks
query <domain>
	get information about a specific email domain
add <domain>
	add the domain to the email domain blocklist
remove <domain>
	remove the domain from the email domain blocklist
<command> help
	show more information about a specific command
""",
		Show = """
Lists all email domain blocks.

Usage:
	cit email-domain-blocks show
""",
		Query = """
Shows information about a specific email domain.

Usage:
	cit email-domain-blocks query <domain>

Where:
	<domain> is a domain on the email domain blocklist.
""",
		Add = """
Add a domain to the email domain blocklist.

Usage:
	cit email-domain-blocks add <domain>

Where:
	<domain> is the domain to add.
""",
		Remove = """
Remove a domain from the email domain blocklist.

Usage:
	cit email-domain-blocks <domain>

Where:
	<domain> is the domain to remove.
""";
}