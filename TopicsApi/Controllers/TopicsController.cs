namespace TopicsApi.Controllers;

[ApiController]
public class TopicsController : ControllerBase
{
    private readonly IProvideTopicsData _topicsData;

    public TopicsController(IProvideTopicsData topicsData)
    {
        _topicsData = topicsData;
    }


    [HttpPost("topics")]
    public async Task<ActionResult> AddTopicAsync([FromBody] PostTopicRequestModel request)
    {

        // 3. If it IS valid
        //      a) do the work (side effect). (for us, add it to the database), etc.

        GetTopicListItemModel response = await _topicsData.AddTopicAsync(request);
        //      b) return a
        //         201 Created status code.
        //         Add a Location header to the response with the URI of the new resource (Location: http://localhost:1337/topics/3)
        //         Maybe just give them a copy of what they'd get from that URI.
        // 201 SHOULD have a Location Header, and a copy of the object that was created.
        return CreatedAtRoute("topics.getbyidasync", new { topicId = response.id }, response);
    }


    // GET /blogs/{year:int}/{month:int:range(1,12)}/{day:int:range(1,31)}
    // Note: This is not needed for our application. For reference only
    // GET /topics/99 => 200 with that document || 404
    // GET /topics/dog => 404
    [HttpGet("topics/{topicId:int}", Name ="topics.getbyidasync")]
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
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 10)]
    public async Task<ActionResult> GetTopicsAsync()
    {
        GetTopicsModel response = await _topicsData.GetAllTopics();
        return Ok(response);
    }
}
