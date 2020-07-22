using McMaster.Extensions.CommandLineUtils;
using SshScript.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SshScript.Commands.Docker
{
    [Command(Name = "docker-compose", Description = "Run docker-compose commands in a remote machine via SSH")]
    public class DockerComposeSubCommand : BaseCommand
    {
        private readonly ISshService _sshService;


        [Option(CommandOptionType.SingleValue, ShortName = "h", LongName = "host", Description = "SSH Host", ValueName = "host", ShowInHelpText = true)]
        public string Host { get; set; }

        [Option(CommandOptionType.SingleValue, ShortName = "u", LongName = "username", Description = "SSH Username", ValueName = "username", ShowInHelpText = true)]
        public string Username { get; set; }

        [Option(CommandOptionType.SingleValue, ShortName = "p", LongName = "password", Description = "SSH Password", ValueName = "password", ShowInHelpText = true)]
        public string Password { get; set; }

        [Option(CommandOptionType.SingleValue, ShortName = "c", LongName = "command", Description = "Docker Compose Command", ValueName = "command", ShowInHelpText = true)]
        public string Command { get; set; }

        public DockerComposeSubCommand(ISshService sshService)
        {
            _sshService = sshService;
        }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            if (string.IsNullOrEmpty(Host) ||
                string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Password) ||
                string.IsNullOrEmpty(Command)
                )
            {
                ConsoleWriter.Error("\nInvalid command\n");
                app.ShowHelp();
                return 0;
            }

            try
            {

                _sshService.Init(Host, Username, Password);

                await _sshService.ExecuteCommand($"sudo docker-compose {Command}");

                _sshService.Dispose();

                ConsoleWriter.Success("\nSuccess!");
            }
            catch (Exception ex)
            {
                ConsoleWriter.Error(ex.Message);
            }

            return 1;
        }
    }
}
