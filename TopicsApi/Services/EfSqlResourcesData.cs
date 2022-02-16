using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TopicsApi.Services;

public class EfSqlResourcesData : IProvideResourcesData
{

    private readonly TopicsDataContext _context;
    private readonly IMapper _mapper;
    private readonly MapperConfiguration _config;

    public EfSqlResourcesData(TopicsDataContext context, IMapper mapper, MapperConfiguration config)
    {
        _context = context;
        _mapper = mapper;
        _config = config;
    }

    public async Task<Maybe<List<ResourceItemModel>>> GetResourcesByTopic(int id)
    {
        // Make sure that topic exists.
        // If it doesn't, return the appropriate Maybe
        // If it does, return all the Resources for that topic
        // Todo: There is going to be a slight bug below. See if you can spot it.
        var topicExists = await _context.Topics!.Where(t => t.Id == id).CountAsync();

        if(topicExists == 1)
        {
            var data = await _context.Resources!.Where(t => t.Topic.Id == id).ProjectTo<ResourceItemModel>(_config).ToListAsync();
            return new Maybe<List<ResourceItemModel>>(true, data);
        } else
        {
            return new Maybe<List<ResourceItemModel>>(false, null);
        }
            

    }
}
