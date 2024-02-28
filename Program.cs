using Spectre.Console;
using Spectre.Console.Cli;
using SshScript.Commands;
using System;

// Need to register the code pages provider for code that parses
// and later needs ISO-8859-2
System.Text.Encoding.RegisterProvider(
    System.Text.CodePagesEncodingProvider.Instance);
// Test that it loads
_ = System.Text.Encoding.GetEncoding("ISO-8859-2");

var app = new CommandApp();
app.Configure((config) =>
{
    config.CaseSensitivity(CaseSensitivity.None);
    config.SetApplicationName("SshScript");
    config.ValidateExamples();

    config.AddCommand<ExecCommand>("exec").WithDescription("Execute command on any host via SSH");
    config.AddCommand<CopyCommand>("copy").WithDescription("Copy folders or files to a remote machine via SSH");
});

try
{

    await app.RunAsync(args);

}
catch (Exception ex)
{
    AnsiConsole.WriteException(ex);
}
