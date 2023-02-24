namespace CiT.CLI.Info;

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
