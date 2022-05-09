using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Renci.SshNet;
using Serilog;
using SshScript.Commands;
using SshScript.Services;
using System;
using System.IO;
using System.Threading.Tasks;

var builder = new HostBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddScoped<ISshService, SshService>();
    });

try
{
    await builder.RunCommandLineApplicationAsync<MainCommand>(args);
}
catch (Exception ex)
{
    Log.Fatal(ex, "Something wrong happened while trying to start the application");
}
