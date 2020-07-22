@ECHO off

set SSH_HOST=10.0.0.1
set SSH_USERNAME=myusername
set SSH_PASSWORD=mypassword

REM output current directory and update linux VM
call SshScript.exe exec -u %SSH_USERNAME% -p %SSH_PASSWORD% -h %SSH_HOST% -c "pwd && sudo apt update"