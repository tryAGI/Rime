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
    "Rime Coda is available through Microsoft.Extensions.AI.",
    new TextToSpeechOptions
    {
        ModelId = "coda",
        VoiceId = "astra",
        AudioFormat = "mp3",
        Speed = 1.05f,
    });

var audio = response.Contents.OfType<DataContent>().Single();
File.WriteAllBytes("rime.mp3", audio.Data.ToArray());
```

`VoiceId` maps to Rime's `speaker`, and `ModelId` selects `coda`, `mistv3`, or `mistv2`. If neither is specified, the adapter uses the recommended Coda model with the `astra` voice. Rime retired Arcana from its cloud API in August 2026; the server still routes the legacy identifier to Coda, but new code should use `coda` and verify the voice/language pairing against the live catalog.

Use `RimeTextToSpeechPropertyNames` for Rime-specific controls:

```csharp
var response = await ttsClient.GetAudioAsync(
    "Use a fixed WAV sample rate and pronunciation controls.",
    new TextToSpeechOptions
    {
        ModelId = "coda",
        VoiceId = "astra",
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
        ModelId = "coda",
        VoiceId = "astra",
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
