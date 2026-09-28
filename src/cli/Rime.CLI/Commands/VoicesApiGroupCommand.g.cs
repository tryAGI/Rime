#nullable enable

using System.CommandLine;

namespace Rime.CLI.Commands;

internal static partial class VoicesApiGroupCommand
{
    static partial void CustomizeCommand(ref Command command);

    public static Command Create()
    {
        var command = new Command(@"voices", @"Voices endpoint commands.");
                         command.Subcommands.Add(VoicesListVoiceDetailsCommandApiCommand.Create());
                         command.Subcommands.Add(VoicesListVoicesCommandApiCommand.Create());
        CustomizeCommand(ref command);
        return command;
    }
}