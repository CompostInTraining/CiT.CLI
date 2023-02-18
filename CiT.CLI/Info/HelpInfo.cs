using System;

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
public static class IpAddressBlocks
{
	public const string
		Main = """
Shows information about IP address rules.

Usage:
	cit ip-blocks <command> [<Arg>...]

Available Commands:

show
	show all IP address rules
query <address>
	show information about a specific ip address rule
add <address> <severity>
	add an IP address to the ruleset
remove <address>
	remove an IP address from the ruleset
""",
		Show = """
Lists all IP addresses in the ruleset.

Usage:
	cit ip-blocks show
""",
		Query = """
Shows information about a specific IP address rule.

Usage:
	cit ip-blocks <address>

Where:
	<address> is the address in the ruleset.

""",
		Add = """
Add an IP address to the ruleset.

Usage:
	cit ip-blocks add <address> <severity>

Where:
	<address> is the address to add.
	<severity> is the rule severity, should be one of
	  - "sign_up_requires_approval", or
	  - "sign_up_block", or
	  - "no_access"
""",
		Remove = """
Remove an IP address from the ruleset.

Usage:
	cit ip-blocks remove <address>

Where:
	<address> is the address to remove.
""";
}
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
