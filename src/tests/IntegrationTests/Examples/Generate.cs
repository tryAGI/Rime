/*
order: 10
title: Generate Speech
slug: generate-speech

Synthesize speech from text using Rime AI's Mist v3 (ultra-low latency) or
Arcana (conversational) models. The audio bytes are returned directly.
*/

namespace Rime.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public async Task Example_GenerateSpeech()
    {
        using var client = GetAuthenticatedClient();

        //// Synthesize a short greeting using the Mist v3 model for low-latency streaming.
        var audio = await client.TextToSpeech.CreateTtsAsync(
            speaker: "cove",
            text: "Hello from Rime AI!",
            modelId: TtsRequestModelId.Mistv3,
            lang: "eng");

        //// The response is raw audio bytes in the format chosen by the Accept header
        //// (defaults to MP3). You can write them to disk or pipe them to an audio player.
        audio.Should().NotBeNull();
        audio.Length.Should().BeGreaterThan(0);
    }
}
