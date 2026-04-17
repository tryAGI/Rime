namespace Rime.IntegrationTests;

[TestClass]
public partial class Tests
{
    private static RimeClient GetAuthenticatedClient()
    {
        var apiKey =
            Environment.GetEnvironmentVariable("RIME_API_KEY") is { Length: > 0 } apiKeyValue
                ? apiKeyValue
                : throw new AssertInconclusiveException("RIME_API_KEY environment variable is not found.");

        var client = new RimeClient(apiKey);
        
        return client;
    }
}
