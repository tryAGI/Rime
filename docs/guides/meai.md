# Microsoft.Extensions.AI Integration

!!! tip "Cross-SDK comparison"
    See the [centralized MEAI documentation](https://tryagi.github.io/docs/meai/) for feature matrices and comparisons across all tryAGI SDKs.

The Rime SDK implements `ITextToSpeechClient` and provides tool wrappers for [Microsoft.Extensions.AI](https://learn.microsoft.com/en-us/dotnet/ai/microsoft-extensions-ai), enabling seamless interoperability with the unified .NET AI abstractions.

## Installation

```bash
dotnet add package Rime
```

## Usage

```csharp
using Microsoft.Extensions.AI;
using Rime;

using var client = new RimeClient(
    apiKey: Environment.GetEnvironmentVariable("RIME_API_KEY")!);

ITextToSpeechClient ttsClient = client;

var response = await ttsClient.GetAudioAsync(
    "Rime Mist v3 is available through Microsoft.Extensions.AI.",
    new TextToSpeechOptions
    {
        ModelId = "mistv3",
        VoiceId = "cove",
        AudioFormat = "mp3",
        Speed = 1.05f,
    });

var audio = response.Contents.OfType<DataContent>().Single();
File.WriteAllBytes("rime.mp3", audio.Data.ToArray());
```

`VoiceId` maps to Rime's `speaker`, and `ModelId` selects `mistv3`, `mistv2`, or `arcana`. If no voice is specified, the adapter defaults to `cove` to match the SDK examples.

Use `RimeTextToSpeechPropertyNames` for Rime-specific controls:

```csharp
var response = await ttsClient.GetAudioAsync(
    "Use a fixed WAV sample rate and pronunciation controls.",
    new TextToSpeechOptions
    {
        ModelId = "mistv3",
        VoiceId = "cove",
        AudioFormat = "wav",
        Language = "en-US",
        AdditionalProperties = new()
        {
            [RimeTextToSpeechPropertyNames.SamplingRate] = 24000,
            [RimeTextToSpeechPropertyNames.PauseBetweenBrackets] = true,
        },
    });
```

Stream response bytes through the same MEAI interface:

```csharp
await foreach (var update in ttsClient.GetStreamingAudioAsync(
    "Read Rime audio chunks through MEAI.",
    new TextToSpeechOptions
    {
        ModelId = "mistv3",
        VoiceId = "cove",
        AudioFormat = "mp3",
    }))
{
    foreach (var chunk in update.Contents.OfType<DataContent>())
    {
        Console.WriteLine($"{update.Kind}: {chunk.Data.Length} bytes");
    }
}
```

## Next Steps

- Check the [Examples](../index.md) for complete working code
- See the [centralized MEAI docs](https://tryagi.github.io/docs/meai/) for cross-SDK comparisons
- Visit the [Microsoft.Extensions.AI documentation](https://learn.microsoft.com/en-us/dotnet/ai/microsoft-extensions-ai) for framework details
