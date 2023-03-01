using AutoMapper;
using mvp_api.Dto.Item;
using mvp_api.Models;

namespace mvp_api.Profiles;

public class ItemProfile : Profile
{
    public ItemProfile() 
    {
        CreateMap<CreateItensDto, Item>();
        CreateMap<UpdateItensDto, Item>();
        CreateMap<Item, UpdateItensDto>();
        CreateMap<Item, ReadItensDto>();
    }
}
