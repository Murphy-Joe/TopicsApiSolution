using AutoMapper;

namespace TopicsApi.AutomapperProfiles;

public class ResourcesProfile : Profile
{
    public ResourcesProfile()
    {
        CreateMap<Resource, ResourceItemModel>()
            .ForMember(dest => dest.id, cfg => cfg.MapFrom(src => src.Id.ToString()));
    }
}
