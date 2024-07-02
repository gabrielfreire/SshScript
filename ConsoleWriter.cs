using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace SshScript
{
    public static class ConsoleWriter
    {
        public static void Output(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void Info(string message)
        {
            AnsiConsole.MarkupLine($"[white]{message}[/]");
        }
        public static void Error(string message)
        {
            AnsiConsole.MarkupLine($"[red]{message}[/]");
        }
        public static void Warning(string message)
        {
            AnsiConsole.MarkupLine($"[yellow]{message}[/]");
        }
        public static void Success(string message)
        {
            AnsiConsole.MarkupLine($"[green]{message}[/]");
        }
        public static void Primary(string message)
        {
            AnsiConsole.MarkupLine($"[blue]{message}[/]");
        }
    }
}
