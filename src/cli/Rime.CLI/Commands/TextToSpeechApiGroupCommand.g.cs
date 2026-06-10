#nullable enable

using System.CommandLine;

namespace Rime.CLI.Commands;

internal static class TextToSpeechApiGroupCommand
{
    public static Command Create()
    {
        var command = new Command(@"text-to-speech", @"TextToSpeech endpoint commands.");
                         command.Subcommands.Add(TextToSpeechCreateTtsCommandApiCommand.Create());
        return command;
    }
}