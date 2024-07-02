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

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        private string? _sshHost;
        private string? _sshUsername;
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public void Init(string sshHost, string sshUsername, string sshPassword)
        {
            try
            {
                _sshHost = sshHost;
                _sshUsername = sshUsername;
                
                var connectionInfo = new ConnectionInfo(
                    sshHost, 
                    sshUsername, 
                    new PasswordAuthenticationMethod(sshUsername, sshPassword));
                _sfClient = new SftpClient(connectionInfo);
                _sshClient = new SshClient(connectionInfo);

                _sfClient.Connect();
                _sshClient.Connect();

                _connected = true;

                //ConsoleWriter.Success($"(CONNECTED) {sshUsername}@{sshHost}");
            }
            catch
            {
                throw;
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
                ConsoleWriter.Error($"Removing the existing file @ {toPath}");
                _sfClient.DeleteFile(toPath);
            }

            using (var fileStream = new FileStream(fromPath, FileMode.Open, FileAccess.Read))
            {
                ConsoleWriter.Warning($"Creating file @ {toPath}");

                var _result = _sfClient.BeginUploadFile(fileStream, toPath);
                while (!_result.IsCompleted)
                {
                    ConsoleWriter.Primary($"Uploading...");
                    await Task.Delay(200);
                }

                ConsoleWriter.Warning($"File created @ {toPath}");
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

                ConsoleWriter.Warning($"{_sshUsername}@{_sshHost}: $ {_command.CommandText}");
                ConsoleWriter.Output($"{_output}");
            }
            catch
            {
                throw;
            }

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _sfClient.Dispose();
            _sshClient.Dispose();
        }
    }
}
