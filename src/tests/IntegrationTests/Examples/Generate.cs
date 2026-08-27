/*
order: 10
title: Generate Speech
slug: generate-speech

Synthesize speech from text using Rime AI's Coda (flagship quality) or
Mist v3 (ultra-low latency) models. The audio bytes are returned directly.
*/

namespace Rime.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public async Task Example_GenerateSpeech()
    {
        using var client = GetAuthenticatedClient();

        //// Synthesize a short greeting using Rime's flagship Coda model.
        var audio = await client.TextToSpeech.CreateTtsAsync(
            speaker: "astra",
            text: "Hello from Rime AI!",
            modelId: TtsRequestModelId.Coda,
            lang: "en");

        //// The response is raw audio bytes in the format chosen by the Accept header
        //// (defaults to MP3). You can write them to disk or pipe them to an audio player.
        audio.Should().NotBeNull();
        audio.Length.Should().BeGreaterThan(0);
    }
}
