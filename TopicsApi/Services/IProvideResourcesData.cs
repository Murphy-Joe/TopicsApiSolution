namespace TopicsApi.Services;

public interface IProvideResourcesData
{
    Task<Maybe<List<ResourceItemModel>>> GetResourcesByTopic(int id);
}
