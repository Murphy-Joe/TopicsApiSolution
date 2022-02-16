namespace TopicsApi.Services;

public class OnCallApiHttpClient
{
    private readonly HttpClient _httpClient;

    public OnCallApiHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetCurrentDeveloperModel> GetTheOnCallDeveloperAsync()
    {
        var response = await _httpClient.GetAsync("/");
        response.EnsureSuccessStatusCode(); /// Throw an exception here!
        var content = await response.Content.ReadFromJsonAsync<GetCurrentDeveloperModel>();

        return content!;

    }
}
