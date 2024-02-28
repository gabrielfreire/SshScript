using Spectre.Console.Cli;
using SshScript.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace SshScript.Commands
{
    public class CopyCommand : AsyncCommand<CopyCommand.CopyCommandSettings>
    {
        public class CopyCommandSettings : CommandSettings
        {
            [CommandOption("-h|--host")]
            [Description("SSH Host")]
            public string Host { get; set; }

            [CommandOption("-u|--username")]
            [Description("SSH Username")]
            public string Username { get; set; }

            [CommandOption("-p|--password")]
            [Description("SSH Password")]
            public string Password { get; set; }

            [CommandOption("-f|--file")]
            [Description("Path of the file to copy")]
            public string File { get; set; }

            [CommandOption("-d|--destination")]
            [Description("Destination path of the file")]
            public string Destination { get; set; }
        }

        private readonly ISshService _sshService;

        public CopyCommand()
        {
            _sshService = new SshService();
        }

        public override async Task<int> ExecuteAsync(CommandContext context, CopyCommandSettings settings)
        {
            try
            {

                _sshService.Init(settings.Host, settings.Username, settings.Password);

                await _sshService.CopyFile(settings.File, settings.Destination);
                await _sshService.ExecuteCommand($"ls -la {settings.Destination.Substring(0, settings.Destination.LastIndexOf("/"))}");

                _sshService.Dispose();
                ConsoleWriter.Success("\nSuccess!");
            }
            catch (Exception ex)
            {
                ConsoleWriter.Error(ex.Message);
            }
            return 0;
        }
    }
}
