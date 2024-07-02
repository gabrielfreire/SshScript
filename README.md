# SSH Script
CLI application to perform remotel tasks via SSH using username/password authentication

# Requirements to run

- Windows 10/Linux/Mac

# Requirements to build
- .NET 8 SDK

# Build

1. Clone repo
2. run `publish.ps1`
3. Executable will be at `dist/SshScript.exe`

```powershell
$ git clone https://github.com/gabrielfreire/SshScript.git
$ cd SshScript
$ publish.ps1
$ dist/SshScript.exe --help
```

# Why ?

I just wanted to automate remote tasks on multiple VMs and stuff like that without having to always explicitly ssh into them.

**Example:** I don't want to have to SSH into my VM in the cloud in order to create some files, perform an update or `pull` latest docker images and
i'd like to automate these tasks.

1. Let's run commands via ssh to update our VM

	Example `batch` script that performs update on linux machine
	```batch
	@ECHO off

	set SSH_HOST=10.0.0.1
	set SSH_USERNAME=myusername
	set SSH_PASSWORD=mypassword

	REM output current directory and update linux VM
	call SshScript.exe exec -u %SSH_USERNAME% -p %SSH_PASSWORD% -h %SSH_HOST% -c "sudo apt update -y && sudo apt upgrade -y"
	```


2. I want to pull the latest images and re-run my docker-compose configuration on a remote VM
	
	Example of `batch` script to manipulate docker images and run containers
	```batch
	@ECHO off

	set SSH_HOST=10.0.0.1
	set SSH_USERNAME=myusername
	set SSH_PASSWORD=mypassword

	REM putput current and list folders (optional)
	call SshScript.exe exec -u %SSH_USERNAME% -p %SSH_PASSWORD% -h %SSH_HOST% -c "pwd && ls -la /home/me"

	REM update my docker images
	call SshScript.exe exec -u %SSH_USERNAME% -p %SSH_PASSWORD% -h %SSH_HOST% -c "docker-compose -f /home/me/docker-compose.yml pull"

	REM run docker images
	call SshScript.exe exec -u %SSH_USERNAME% -p %SSH_PASSWORD% -h %SSH_HOST% -c "docker-compose -f /home/me/docker-compose.yml run -d"

	```

# Usage

For any command you want to run, you must provide SSH username and password credentials, see examples below.

```bash
$ SshScript.exe -h

USAGE:
    SshScript [OPTIONS] <COMMAND>

OPTIONS:
    -h, --help       Prints help information
    -v, --version    Prints version information

COMMANDS:
    exec    Execute command on any host via SSH
    copy    Copy folders or files to a remote machine via SSH
```

### exec examples

- list folders
	- `SshScript.exe exec -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "ls -la /home"`
- Make directory
	- `SshScript.exe exec -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "mkdir /home/mydir"`
- list containers
	- `SshScript.exe exec -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "docker ps"`
- list images
	- `SshScript.exe exec -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "docker images"`
- list containers
	- `SshScript.exe exec -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "docker-compose build"`
- list images
	- `SshScript.exe exec -u yoursshusername -p yoursshpassword -h 10.0.0.1 -c "docker-compose up -d"`

### copy examples

- copy README.md
	- `SshScript.exe copy -u yoursshusername -p yoursshpassword -h 10.0.0.1 -f .\README.md -d /home/yoursshusername/README.md`
	
# TODO
- maybe a config file with credentials to simplify commands
- maybe add option to read creds from env. variables


# LICENSE
SSHScript is MIT Licensed

MIT License

Copyright (c) 2020 Gabriel Augusto de Lima Freire

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.