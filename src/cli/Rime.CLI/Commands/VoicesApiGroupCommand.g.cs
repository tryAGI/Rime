#nullable enable

using System.CommandLine;

namespace Rime.CLI.Commands;

internal static class VoicesApiGroupCommand
{
    public static Command Create()
    {
        var command = new Command(@"voices", @"Voices endpoint commands.");
                         command.Subcommands.Add(VoicesListVoiceDetailsCommandApiCommand.Create());
                         command.Subcommands.Add(VoicesListVoicesCommandApiCommand.Create());
        return command;
    }
}