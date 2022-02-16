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
        try
        {
            return await _client.GetTheOnCallDeveloperAsync();
        }
        catch (Exception)
        {
            // a bit of a corny classroom plan "B" but the point is you can't just send a 500 to the client if someone 
            // else's API is down.
            return new GetCurrentDeveloperModel("Unable to Provide the Current  On Call Developer -  Call the Main Help Desk", "800-HELP", "help@company.com", DateTime.Now);
        }
    }
}
