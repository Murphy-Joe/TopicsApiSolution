namespace TopicsApi.Controllers;

public class TopicsController : ControllerBase
{
    private readonly IProvideTopicsData _topicsData;

    public TopicsController(IProvideTopicsData topicsData)
    {
        _topicsData = topicsData;
    }


    // GET /blogs/{year:int}/{month:int:range(1,12)}/{day:int:range(1,31)}
    // Note: This is not needed for our application. For reference only
    // GET /topics/99 => 200 with that document || 404
    // GET /topics/dog => 404
    [HttpGet("topics/{topicId:int}")]
    public async Task<ActionResult> GetTopicByIdAsync(int topicId)
    {
        Maybe<GetTopicListItemModel> response = await _topicsData.GetTopicByIdAsync(topicId);
       
        return response.hasValue switch
        {
            false => NotFound(),
            true => Ok(response.value), 
        };
       
    }


    [HttpGet("topics")]
    public async Task<ActionResult> GetTopicsAsync()
    {
        GetTopicsModel response = await _topicsData.GetAllTopics();
        return Ok(response);
    }
}
