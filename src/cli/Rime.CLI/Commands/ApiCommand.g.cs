#nullable enable

using System.CommandLine;

namespace Rime.CLI.Commands;

internal static partial class ApiCommand
{
    static partial void CustomizeCommand(ref Command command);

    public static Command Create()
    {
        var command = new Command("api", "Generated endpoint commands.");

                         command.Subcommands.Add(TextToSpeechApiGroupCommand.Create());
                         command.Subcommands.Add(VoicesApiGroupCommand.Create());
        CustomizeCommand(ref command);
        return command;
    }
}