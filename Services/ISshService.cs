using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SshScript.Services
{
    public interface ISshService : IDisposable
    {
        void Init(string sshHost, string sshUsername, string sshPassword);

        Task CopyFile(string fromPath, string toPath);

        Task ExecuteCommand(string command);
    }
}
