[![.NET](https://github.com/CompostInTraining/CiT.CLI/actions/workflows/dotnet.yml/badge.svg)](https://github.com/CompostInTraining/CiT.CLI/actions/workflows/dotnet.yml)
[![.NET Release](https://github.com/CompostInTraining/CiT.CLI/actions/workflows/dotnet-release.yml/badge.svg)](https://github.com/CompostInTraining/CiT.CLI/actions/workflows/dotnet-release.yml)
[![GitHub release (latest SemVer including pre-releases)](https://img.shields.io/github/v/release/CompostInTraining/CiT.CLI?include_prereleases)](https://github.com/CompostInTraining/CiT.CLI/releases/latest)

# CiT.CLI

A CLI for administering the CompostInTraining.club Mastodon instance.

## Usage

```shell
cit <command> <subcommand> [parameters]
```

### Available Commands

#### domain-blocks

```shell
cit domain-blocks <command> [<Arg>...]
```

`domain-blocks` commands:

* `show` - list all domain blocks
* `query <domain>` - query the blocklist for a given domain
* `add <domain> [<severity> <comment>]` - add a domain to the blocklist,
  optionally specifying the severity and providing a comment.
    * `<severity>` should be one of `suspend`, `silence`, or `noop` per
      the [Mastodon docs](https://docs.joinmastodon.org/methods/admin/domain_blocks/#form-data-parameters)
* `remove <domain>` - remove a domain from the blocklist

#### email-domain-blocks

```shell
cit email-domain-blocks <command> [<Arg>...]
```

`email-domain-blocks` commands:

* `show` - list all email domain blocks
* `query <domain>` - query the blocklist for a given domain
* `add <domain>` - add an email domain to the blocklist
* `remove <domain>` - remove an email domain from the blocklist

#### ip-blocks

```shell
cit ip-blocks <command> [<Arg>...]
```

`ip-blocks` commands:

* `show` - list all ip rules
* `query <address>` - query the ruleset for a given address
* `add <address> <severity>` - add an ip address to the ruleset
  * `<severity>` should be one of `sign_up_requires_approval`, `sign_up_block`, or `no_access` per
    the [Mastodon docs](https://docs.joinmastodon.org/methods/admin/ip_blocks/#form-data-parameters)
* `remove <address>` - remove an ip address from the ruleset
