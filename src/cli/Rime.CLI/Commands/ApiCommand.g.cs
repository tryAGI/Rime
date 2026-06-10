#nullable enable

using System.CommandLine;

namespace Rime.CLI.Commands;

internal static class ApiCommand
{
    public static Command Create()
    {
        var command = new Command("api", "Generated endpoint commands.");

                         command.Subcommands.Add(TextToSpeechApiGroupCommand.Create());
                         command.Subcommands.Add(VoicesApiGroupCommand.Create());
        return command;
    }
}