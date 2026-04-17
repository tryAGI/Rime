/*
order: 30
title: MEAI Tools
slug: meai-tools

Use Rime AI as function-calling tools with any Microsoft.Extensions.AI IChatClient.
*/

using Microsoft.Extensions.AI;

namespace Rime.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public void Example_CreateMeaiTools()
    {
        using var client = GetAuthenticatedClient();

        //// Create AIFunction tools that any IChatClient can invoke via function calling.
        AIFunction tts = client.AsTextToSpeechTool(defaultSpeaker: "cove", defaultModel: "mistv3");
        AIFunction listVoices = client.AsListVoicesTool();
        AIFunction listVoiceDetails = client.AsListVoiceDetailsTool();

        //// The tools expose their names and descriptions to the model.
        tts.Name.Should().Be("RimeTextToSpeech");
        listVoices.Name.Should().Be("RimeListVoices");
        listVoiceDetails.Name.Should().Be("RimeListVoiceDetails");
    }
}
