using Microsoft.Extensions.Hosting;
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
    public class ExecCommand : AsyncCommand<ExecCommand.ExecCommandSettings>
    {
        public class ExecCommandSettings : CommandSettings
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

            [CommandOption("-c|--command")]
            [Description("SSH Command")]
            public string Command { get; set; }
        }

        private readonly ISshService _sshService;

        public ExecCommand()
        {
            _sshService = new SshService();
        }

        public override async Task<int> ExecuteAsync(CommandContext context, ExecCommandSettings settings)
        {
            try
            {

                _sshService.Init(settings.Host, settings.Username, settings.Password);

                await _sshService.ExecuteCommand(settings.Command);


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
