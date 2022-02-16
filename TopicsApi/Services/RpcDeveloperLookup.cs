namespace TopicsApi.Services;

public class RpcDeveloperLookup : ILookupOnCallDevelopers
{
    private readonly HttpClient _httpClient;

    public RpcDeveloperLookup(HttpClient httpClient)
    {
        _httpClient = httpClient; // NEVER EVER EVER call "New" on the HTTPClient
    }

    public async Task<GetCurrentDeveloperModel> GetCurrentOnCallDeveloperAsync()
    {
        var response = await _httpClient.GetAsync("/");

        var content = await response.Content.ReadFromJsonAsync<GetCurrentDeveloperModel>();

        return content!;
    }
}
