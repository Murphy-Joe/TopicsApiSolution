namespace TopicsApi.Controllers;

public class ResourcesController : ControllerBase
{

    private readonly IProvideResourcesData _service;

    public ResourcesController(IProvideResourcesData service)
    {
        _service = service;
    }

    [HttpGet("topics/{id:int}/resources")]
    public async Task<ActionResult> GetResourcesForTopicAsync(int id)
    {
        Maybe<List<ResourceItemModel>> data = await _service.GetResourcesByTopic(id);

        return data.hasValue switch
        {
            true => Ok(new { data = data.value }),
            false => NotFound("No Topic with that Id")
        };
    }
}
