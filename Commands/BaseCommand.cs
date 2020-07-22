using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SshScript.Commands
{
    [HelpOption("--help")]
    public class BaseCommand
    {
        protected virtual Task<int> OnExecute(CommandLineApplication app)
        {
            return Task.FromResult(0);
        }
    }
}
