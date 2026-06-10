#nullable enable

using System.CommandLine;
using Rime.CLI;
using Rime.CLI.Commands;

var rootCommand = new RootCommand(@"CLI tool for the Rime SDK.");
rootCommand.Options.Add(CliOptions.ApiKey);
rootCommand.Options.Add(CliOptions.BaseUrl);
rootCommand.Options.Add(CliOptions.Json);
rootCommand.Options.Add(CliOptions.Output);
rootCommand.Options.Add(CliOptions.OutputDirectory);
rootCommand.Subcommands.Add(AuthCommand.Create());
rootCommand.Subcommands.Add(TextToSpeechApiGroupCommand.Create());
rootCommand.Subcommands.Add(VoicesApiGroupCommand.Create());

return await rootCommand.Parse(args).InvokeAsync().ConfigureAwait(false);