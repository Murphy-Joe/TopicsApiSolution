namespace TopicsApi.Services;

public class FakeDeveloperLookup : ILookupOnCallDevelopers
{
    public async Task<GetCurrentDeveloperModel> GetCurrentOnCallDeveloperAsync()
    {
        // Here is where you'd do the "real work" - get it from another service, etc...
        return new GetCurrentDeveloperModel("Joe Schmidt", "777-1212", "joe@aol.com", DateTime.Now);
    }
}
