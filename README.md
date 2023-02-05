# CiT-CLI

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
    * `<severity>` should be one of 'suspend', 'silence', or 'noop' per
      the [Mastodon docs](https://docs.joinmastodon.org/methods/admin/domain_blocks/#form-data-parameters)