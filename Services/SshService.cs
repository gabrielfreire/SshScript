using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Serilog;

namespace SshScript.Services
{
    public class SshService : ISshService
    {
        private SftpClient _sfClient;
        private SshClient _sshClient;
        private bool _connected = false;

        public void Init(string sshHost, string sshUsername, string sshPassword)
        {
            try
            {

                var connectionInfo = new ConnectionInfo(sshHost, sshUsername, new PasswordAuthenticationMethod(sshUsername, sshPassword));
                _sfClient = new SftpClient(connectionInfo);
                _sshClient = new SshClient(connectionInfo);

                _sfClient.Connect();
                _sshClient.Connect();

                _connected = true;

                ConsoleWriter.Success($"\n Connected to {sshHost} as {sshUsername} \n");
            }
            catch (Exception ex)
            {
                ConsoleWriter.Error($"\n Failed to connect to {sshHost} as {sshUsername} \nThrew Exception: {ex.Message}");

            }
        }

        public async Task CopyFile(string fromPath, string toPath)
        {
            if (!_connected)
                throw new InvalidOperationException("SSH not connected");

            if (string.IsNullOrEmpty(fromPath) || string.IsNullOrEmpty(toPath))
                throw new InvalidOperationException("Invalid operation: please provide correct paths");

            if (_sfClient.Exists(toPath))
            {
                ConsoleWriter.Info($"Removing existing docker compose file @ [ {toPath} ]");
                _sfClient.DeleteFile(toPath);
            }

            using (var fileStream = new FileStream(fromPath, FileMode.Open, FileAccess.Read))
            {
                ConsoleWriter.Info($"Creating new docker compose file @ [ {toPath} ]");

                var _result = _sfClient.BeginUploadFile(fileStream, toPath);
                while (!_result.IsCompleted)
                {
                    await Task.Delay(1000);
                }

                ConsoleWriter.Info($"Docker compose file created @ [ {toPath} ]");
            }
            
        }

        public Task ExecuteCommand(string command)
        {
            if (!_connected)
                throw new InvalidOperationException("SSH not connected");

            try
            {
                var _command = _sshClient.CreateCommand(command);
                var _output = _command.Execute();

                ConsoleWriter.Info($"command: {_command.CommandText}");
                ConsoleWriter.Primary($"output:\n{_output}");

            }
            catch (Exception ex)
            {
                ConsoleWriter.Error($"Invalid command {ex.Message}");
            }

            return Task.CompletedTask;
        }

        public void Dispose()
        {

            _sfClient.Disconnect();
            _sfClient.Dispose();

            _sshClient.Disconnect();
            _sshClient.Dispose();

            ConsoleWriter.Success("\nSuccess!\n");
            ConsoleWriter.Success("Closed SSH Connection");
        }
    }
}
