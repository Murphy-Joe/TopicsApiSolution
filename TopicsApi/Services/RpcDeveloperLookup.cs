namespace TopicsApi.Services;

public class RpcDeveloperLookup : ILookupOnCallDevelopers
{
    private readonly OnCallApiHttpClient _client;

    public RpcDeveloperLookup(OnCallApiHttpClient client)
    {
        _client = client;
    }

    public async Task<GetCurrentDeveloperModel> GetCurrentOnCallDeveloperAsync()
    {
        return await _client.GetTheOnCallDeveloperAsync(); 
    }
}
