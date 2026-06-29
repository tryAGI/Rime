#nullable enable
#pragma warning disable MEAI001

using System.Net;
using System.Text.Json;
using Microsoft.Extensions.AI;

namespace Rime.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public void TextToSpeechClient_GetService_Metadata()
    {
        using var client = new RimeClient("test-api-key");
        Microsoft.Extensions.AI.ITextToSpeechClient ttsClient = client;

        var metadata = ttsClient.GetService<TextToSpeechClientMetadata>();

        metadata.Should().NotBeNull();
        metadata!.ProviderName.Should().Be("rime");
        metadata.DefaultModelId.Should().Be("mistv3");
        metadata.ProviderUri.Should().NotBeNull();
        ttsClient.GetService<RimeClient>().Should().BeSameAs(client);
    }

    [TestMethod]
    public async Task TextToSpeechClient_MapsOptionsToTtsRequest()
    {
        var handler = new StaticResponseHandler(HttpStatusCode.OK, [1, 2, 3], "audio/wav");
        using var client = new RimeClient(
            "test-api-key",
            new HttpClient(handler)
            {
                BaseAddress = new Uri(RimeClient.DefaultBaseUrl),
            });
        Microsoft.Extensions.AI.ITextToSpeechClient ttsClient = client;
        TtsRequest? capturedRequest = null;

        var response = await ttsClient.GetAudioAsync(
            "Hello from Rime.",
            new TextToSpeechOptions
            {
                ModelId = "mistv3",
                VoiceId = "cove",
                AudioFormat = "wav",
                Language = "en-US",
                Speed = 1.2f,
                AdditionalProperties = new()
                {
                    [RimeTextToSpeechPropertyNames.SamplingRate] = 24000,
                    [RimeTextToSpeechPropertyNames.PauseBetweenBrackets] = true,
                    [RimeTextToSpeechPropertyNames.InlineSpeedAlpha] = "1.1,0.9",
                },
                RawRepresentationFactory = _ =>
                {
                    capturedRequest = new TtsRequest
                    {
                        Speaker = string.Empty,
                        Text = string.Empty,
                    };

                    return capturedRequest;
                },
            });

        capturedRequest.Should().NotBeNull();
        capturedRequest!.Text.Should().Be("Hello from Rime.");
        capturedRequest.Speaker.Should().Be("cove");
        capturedRequest.ModelId.Should().Be(TtsRequestModelId.Mistv3);
        capturedRequest.Lang.Should().Be("eng");
        capturedRequest.SamplingRate.Should().Be(24000);
        capturedRequest.TimeScaleFactor.Should().BeApproximately(1.2f, 0.00001f);
        capturedRequest.PauseBetweenBrackets.Should().BeTrue();
        capturedRequest.InlineSpeedAlpha.Should().Be("1.1,0.9");

        handler.LastRequest.Should().NotBeNull();
        handler.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/v1/rime-tts");
        handler.LastRequest.Headers.Authorization!.Scheme.Should().Be("Bearer");
        handler.LastRequest.Headers.Authorization.Parameter.Should().Be("test-api-key");
        handler.LastRequest.Headers.Accept.Single().MediaType.Should().Be("audio/wav");

        response.Contents.OfType<DataContent>().Single().Data.ToArray().Should().Equal([1, 2, 3]);
        response.ModelId.Should().Be("mistv3");
        response.AdditionalProperties![RimeTextToSpeechPropertyNames.Accept].Should().Be("audio/wav");

        using var document = JsonDocument.Parse(handler.LastRequestBody!);
        document.RootElement.GetProperty("speaker").GetString().Should().Be("cove");
        document.RootElement.GetProperty("text").GetString().Should().Be("Hello from Rime.");
        document.RootElement.GetProperty("modelId").GetString().Should().Be("mistv3");
    }

    [TestMethod]
    public async Task TextToSpeechClient_StreamsAudioChunks()
    {
        var handler = new StaticResponseHandler(HttpStatusCode.OK, [1, 2, 3, 4], "audio/mpeg");
        using var client = new RimeClient(
            "test-api-key",
            new HttpClient(handler)
            {
                BaseAddress = new Uri(RimeClient.DefaultBaseUrl),
            });
        Microsoft.Extensions.AI.ITextToSpeechClient ttsClient = client;

        var updates = new List<TextToSpeechResponseUpdate>();
        await foreach (var update in ttsClient.GetStreamingAudioAsync(
            "Streaming Rime speech.",
            new TextToSpeechOptions
            {
                ModelId = "mistv3",
                VoiceId = "cove",
                AudioFormat = "mp3",
            }))
        {
            updates.Add(update);
        }

        updates.First().Kind.Should().Be(TextToSpeechResponseUpdateKind.SessionOpen);
        updates.Should().Contain(static update => update.Kind == TextToSpeechResponseUpdateKind.AudioUpdating);
        updates.Should().Contain(static update => update.Kind == TextToSpeechResponseUpdateKind.AudioUpdated);
        updates.Last().Kind.Should().Be(TextToSpeechResponseUpdateKind.SessionClose);
    }

    private sealed class StaticResponseHandler(
        HttpStatusCode statusCode,
        byte[] responseBody,
        string mediaType) : HttpMessageHandler
    {
        public HttpRequestMessage? LastRequest { get; private set; }
        public string? LastRequestBody { get; private set; }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            LastRequest = request;
            LastRequestBody = request.Content is null
                ? null
                : await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return new HttpResponseMessage(statusCode)
            {
                RequestMessage = request,
                Content = new ByteArrayContent(responseBody)
                {
                    Headers =
                    {
                        ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType),
                    },
                },
            };
        }
    }
}
