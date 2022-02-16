namespace TopicsApi.Data;

public class Resource
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Link { get; set; }
    public Topic Topic { get; set; }

}


