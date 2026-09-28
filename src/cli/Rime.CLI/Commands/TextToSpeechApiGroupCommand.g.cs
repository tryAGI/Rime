#nullable enable

using System.CommandLine;

namespace Rime.CLI.Commands;

internal static partial class TextToSpeechApiGroupCommand
{
    static partial void CustomizeCommand(ref Command command);

    public static Command Create()
    {
        var command = new Command(@"text-to-speech", @"TextToSpeech endpoint commands.");
                         command.Subcommands.Add(TextToSpeechCreateTtsCommandApiCommand.Create());
        CustomizeCommand(ref command);
        return command;
    }
}