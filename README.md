# IRCCLIENT

**IRC client library for .NET** — primarily designed for controlling MediaWiki bots over IRC.

[![License: GPL-2.0](https://img.shields.io/badge/License-GPL%202.0-blue.svg)](https://www.gnu.org/licenses/old-licenses/gpl-2.0.en.html)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)](https://dotnet.microsoft.com/)

IRCCLIENT is a Visual Basic .NET library that provides a robust IRC client, tightly integrated with [MWBot.net](https://github.com/MarioFinale/MWBot.net). It is intended for wiki bots that need to be controlled or monitored via IRC channels, with built-in support for command handling, operator authorization, flood protection, and SASL authentication.

## Features

- **IRC Connection Management** — Connect to IRC servers with automatic reconnection.
- **SASL PLAIN Authentication** — Secure login support (`UseSASL` property).
- **Flood Protection** — Configurable message delay (`FloodDelay`) to avoid being kicked.
- **Command System** — Prefix-based commands (default `/`, customizable) with alias support and authorization checks.
- **Operator Authorization** — Restrict sensitive commands to operators listed in an op file.
- **MWBot.net Integration** — Designed to work with `WikiBot.Bot` and `TaskAdmin` for seamless bot control.
- **Config File Driven** — Simple text config file for server, nick, password, and channels.
- **NuGet Package** — Ready to consume as a package (depends on `MWBot.net`).

## Installation

### NuGet

```bash
dotnet add package IRCCLIENT
```

Or via Package Manager Console:

```powershell
Install-Package IRCCLIENT
```

**Dependencies:**
- Target framework: `net10.0`
- Requires `MWBot.net` (automatically pulled as a dependency)

## Quick Start

The library expects a configuration file with four lines:

```
irc.example.org
MyBotNick
myPasswordOrEmpty
#channel1|#channel2
```

### Basic Usage (VB.NET)

```vbnet
Imports IRC
Imports MWBot.net.WikiBot

' Assuming you already have a WikiBot instance and TaskAdmin
Dim bot As Bot = ... ' your MWBot.net Bot
Dim taskAdmin As TaskAdmin = ...
Dim configFile As String = "irc.cfg"
Dim opFile As String = "ops.txt"
Dim prefixes As String() = {"/", "%"}

Dim ircClient As New IRC_Client(
    Cfile:=configFile,
    port:=6667,
    opFile:=opFile,
    bot:=bot,
    taskadmin:=taskAdmin,
    exeversion:="1.0.0",
    exename:="MyWikiBot",
    commandprefixes:=prefixes
)

ircClient.UseSASL = True   ' Enable SASL PLAIN if the server supports it
ircClient.FloodDelay = 700 ' milliseconds between messages

ircClient.StartClient()    ' Starts the connection and message loop (async)
```

### Key Properties & Methods

| Member | Description |
|--------|-------------|
| `NickName` | Current nickname (read-only) |
| `Server` | Connected server (read-only) |
| `UseSASL` | Enable/disable SASL PLAIN authentication |
| `FloodDelay` | Delay between outgoing messages (ms) |
| `StartClient()` | Begins the IRC connection and processing loop |
| `HasExited` | Indicates whether the client has stopped |

## Command System

Commands are resolved via `IRCCommandResolver`. By default prefixes are `/`. Operators (listed in the op file as `nick!user@host` or similar patterns) can execute privileged commands.

The library ships with a set of built-in commands related to bot task management, flood control, culture, and status reporting (see `IRCCommands.vb`).

## Configuration Files

### IRC Config (`irc.cfg`)
```
server.example.com
BotNick
password
#mychannel|#anotherchannel
```

### Operator File (`ops.txt`)
One operator per line in the format expected by `IsOp` (typically `nick!ident@host`).

## License

This project is licensed under the **GPL-2.0-only** license. See the [LICENSE](LICENSE) file for details.

## Author

**MarioFinale**  
GitHub: [@MarioFinale](https://github.com/MarioFinale)

## Related Projects

- [MWBot.net](https://github.com/MarioFinale/MWBot.net) — MediaWiki bot library this client is designed to work with.
- [PeriodiBOT](https://github.com/MarioFinale/PeriodiBOT) — Multipurpose MediaWiki bot that can use this client.

---

*Package release notes and full changelog: see the [commit history](https://github.com/MarioFinale/IRCCLIENT/commits/master/).*
