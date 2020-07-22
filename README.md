# SSH Script
CLI application to perform tasks remotely via SSH using username/password authentication

# Requirements

- [.NET Core 3.1 Runtime 64 bit](https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.6-windows-x64-installer) or [.NET Core 3.1 Runtime 32 bit](https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.6-windows-x86-installer)
- [Download SshScript]()

# Usage

You must provide SSH username and password credentials, see examples below.

## Exec
### list folders
- `SshScript.exe exec -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "ls -la /home"`
### Make directory
- `SshScript.exe exec -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "mkdir /home/mydir"`
## Docker
### list containers
- `SshScript.exe docker -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "ps"`
### list images
- `SshScript.exe docker -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "images"`
## Docker Compose
### list containers
- `SshScript.exe docker-compose -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "build"`
### list images
- `SshScript.exe docker-compose -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "up -d"`

# LICENSE
SSHScript is MIT Licensed