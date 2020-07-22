using McMaster.Extensions.CommandLineUtils;
using SshScript.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SshScript.Commands.Copy
{

    [Command(Name = "copy", Description = "Copy folders or files to a remote machine via SSH")]
    public class CopySubCommand : BaseCommand
    {
        private readonly ISshService _sshService;


        [Option(CommandOptionType.SingleValue, ShortName = "h", LongName = "host", Description = "SSH Host", ValueName = "host", ShowInHelpText = true)]
        public string Host { get; set; }
        
        [Option(CommandOptionType.SingleValue, ShortName = "u", LongName = "username", Description = "SSH Username", ValueName = "username", ShowInHelpText = true)]
        public string Username { get; set; }
        
        [Option(CommandOptionType.SingleValue, ShortName = "p", LongName = "password", Description = "SSH Password", ValueName = "password", ShowInHelpText = true)]
        public string Password { get; set; }

        [Option(CommandOptionType.SingleValue, ShortName = "f", LongName = "file", Description = "Path of the file", ValueName = "file", ShowInHelpText = true)]
        public string File { get; set; }

        [Option(CommandOptionType.SingleValue, ShortName = "d", LongName = "destination", Description = "Destination path of the file", ValueName = "destination", ShowInHelpText = true)]
        public string Destination { get; set; }

        public CopySubCommand(ISshService sshService)
        {
            _sshService = sshService;
        }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            if (string.IsNullOrEmpty(Host)     ||
                string.IsNullOrEmpty(Username) || 
                string.IsNullOrEmpty(Password) || 
                string.IsNullOrEmpty(File)     || 
                string.IsNullOrEmpty(Destination) 
                )
            {
                ConsoleWriter.Error("\nInvalid command\n");
                app.ShowHelp();
                return 0;
            }

            try
            {

                _sshService.Init(Host, Username, Password);

                await _sshService.CopyFile(File, Destination);
                await _sshService.ExecuteCommand($"ls -la {Destination.Substring(0, Destination.LastIndexOf("/"))}");

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
