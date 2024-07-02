# SSH Script
CLI application to perform remotel tasks via SSH using username/password authentication

# Requirements

- Windows 10/Linux/Mac
- .NET 8 SDK

# Installation

1. Clone repo
2. run `publish.ps1`
3. Executable will be at `dist/SshScript.exe`

# Use case
I don't want to have to SSH into my VM in the cloud in order to create some files, perform an update or `pull` latest docker images and
i'd like to automate these tasks.

1. Let's run commands via ssh to update our VM
	- Create a batch/sh script `update_my_vm.bat`

	the contents for your script should look like this
	```batch
	@ECHO off

	set SSH_HOST=10.0.0.1
	set SSH_USERNAME=myusername
	set SSH_PASSWORD=mypassword

	REM output current directory and update linux VM
	call SshScript.exe exec -u %SSH_USERNAME% -p %SSH_PASSWORD% -h %SSH_HOST% -c "pwd && sudo apt update"
	```

	- Now you can setup an automation process that calls this script from time to time or include a call to it in your CI/CD pipeline.

2. I want to pull the latest images and re-run my docker-compose configuration on a remote VM
	- Create a batch script `pull_latest_docker_images_and_start.bat`

	The contents for your script should look like this
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

### Exec

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

### Copy files

- copy README.md
	- `SshScript.exe copy -u yoursshusername -p yoursshpassword -h 10.0.0.1 -f .\README.md -d /home/yoursshusername/README.md`
	
# TODO
- configuration file with credentials to simplify commands

# LICENSE
SSHScript is MIT Licensed