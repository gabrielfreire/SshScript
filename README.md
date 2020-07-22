# SSH Script
CLI application to perform remotel tasks via SSH using username/password authentication

# Requirements

- Windows 10 (the source-code is cross-platform, but i've only published an `.exe` version)
- [.NET Core 3.1 Runtime 64 bit](https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.6-windows-x64-installer) or [.NET Core 3.1 Runtime 32 bit](https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.6-windows-x86-installer)
- [Download SshScript](https://github.com/gabrielfreire/SshScript/raw/master/dist/SshScript.zip)

# Installation

No installation required, just download and extract `SshScript.zip` to anywhere and use it, you can also add the destination to your `PATH` environment variable
for easy usage.

# Usage

For any command you want to run, you must provide SSH username and password credentials, see examples below.

```bash
$ SshScript.exe --help

1.0.0

Usage: SshScript.exe [command] [options]

Options:
  --version       Show version information
  --help          Show help information

Commands:
  copy            Copy folders or files to a remote machine via SSH
  docker          Run docker commands in a remote machine via SSH
  docker-compose  Run docker-compose commands in a remote machine via SSH
  exec            Run any command in a remote machine via SSH

Run 'SshScript.exe [command] --help' for more information about a command.
```

### Exec
- list folders
	- `SshScript.exe exec -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "ls -la /home"`
- Make directory
	- `SshScript.exe exec -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "mkdir /home/mydir"`
### Docker
- list containers
	- `SshScript.exe docker -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "ps"`
- list images
	- `SshScript.exe docker -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "images"`
### Docker Compose
- list containers
	- `SshScript.exe docker-compose -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "build"`
- list images
	- `SshScript.exe docker-compose -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "up -d"`

# TODO
- configuration file with credentials to simplify commands

# LICENSE
SSHScript is MIT Licensed