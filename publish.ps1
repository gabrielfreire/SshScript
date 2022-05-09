if (Test-Path -Path .\dist) {
	rm .\dist\SshScript.exe
}

dotnet publish -c Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true -r win-x64 -o .\dist
rm .\dist\SshScript.pdb