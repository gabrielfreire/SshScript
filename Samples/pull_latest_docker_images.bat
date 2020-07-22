@ECHO off

set SSH_HOST=10.0.0.1
set SSH_USERNAME=myusername
set SSH_PASSWORD=mypassword

REM putput current and list folders (optional)
call SshScript.exe exec -u %SSH_USERNAME% -p %SSH_PASSWORD% -h %SSH_HOST% -c "pwd && ls -la /home/me"

REM update my docker images
call SshScript.exe docker-compose -u %SSH_USERNAME% -p %SSH_PASSWORD% -h %SSH_HOST% -c "-f /home/me/docker-compose.yml pull"

REM run docker images
call SshScript.exe docker-compose -u %SSH_USERNAME% -p %SSH_PASSWORD% -h %SSH_HOST% -c "-f /home/me/docker-compose.yml run -d"