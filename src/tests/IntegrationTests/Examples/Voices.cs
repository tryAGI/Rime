/*
order: 20
title: List Voices
slug: list-voices

List all available Rime AI voice names grouped by model and language.
*/

namespace Rime.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public async Task Example_ListVoices()
    {
        using var client = GetAuthenticatedClient();

        //// Fetch the full voice catalog. The response is a nested dictionary:
        //// modelId -> languageCode -> array of voice names.
        var voices = await client.Voices.ListVoicesAsync();

        voices.Should().NotBeNull();
        voices.Should().NotBeEmpty();

        //// Each model (mistv3, mistv2, arcana) has at least one language bucket.
        voices.Values.Should().OnlyContain(langs => langs.Count > 0);
    }

    [TestMethod]
    public async Task Example_ListVoiceDetails()
    {
        using var client = GetAuthenticatedClient();

        //// Fetch detailed voice metadata (speaker, gender, dialect, genre, etc.).
        var details = await client.Voices.ListVoiceDetailsAsync();

        details.Should().NotBeNull();
        details.Should().NotBeEmpty();
    }
}
