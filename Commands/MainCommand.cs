using McMaster.Extensions.CommandLineUtils;
using SshScript.Commands.Copy;
using SshScript.Commands.Docker;
using SshScript.Commands.Exec;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SshScript.Commands
{
    [Command(Name = "sshutils", OptionsComparison = System.StringComparison.InvariantCultureIgnoreCase)]
    [VersionOptionFromMember("--version", MemberName = nameof(GetVersion))]
    [Subcommand(typeof(CopySubCommand))]
    [Subcommand(typeof(DockerSubCommand))]
    [Subcommand(typeof(DockerComposeSubCommand))]
    [Subcommand(typeof(ExecSubCommand))]
    public class MainCommand : BaseCommand
    {
        protected override Task<int> OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return Task.FromResult(0);
        }
        private static string GetVersion()
            => typeof(MainCommand).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    }
}
