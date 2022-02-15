namespace TopicsApi.Services;

public class EfSqlTopicsData : IProvideTopicsData
{
    private readonly TopicsDataContext _context;

    public EfSqlTopicsData(TopicsDataContext context)
    {
        _context = context;
    }

    public async Task<GetTopicsModel> GetAllTopics()
    {
        var data = await _context.Topics!
            .Select(t => new GetTopicListItemModel(t.Id.ToString(), t.Name, t.Description)).ToListAsync();

        return new GetTopicsModel(data);

    }

    public async Task<Maybe<GetTopicListItemModel>> GetTopicByIdAsync(int topicId)
    {
        var data = await _context.Topics!
            .Where(t => t.Id == topicId) 
            .Select(t => new GetTopicListItemModel(t.Id.ToString(), t.Name, t.Description))
            .SingleOrDefaultAsync();
    
        return data switch
        {
            null => new Maybe<GetTopicListItemModel>(false, null),
            _ => new Maybe<GetTopicListItemModel>(true, data)
        };
    }
}
