using AutoMapper;
using mvp_api.Dto.ItensFatura;
using mvp_api.Models;

namespace mvp_api.Profiles
{
    public class ItemFaturaProfile : Profile
    {
        public ItemFaturaProfile()
        {
            CreateMap<CreateItemFaturaDto, ItemFatura>();
            CreateMap<UpdateItemFaturaDto, ItemFatura>();
            CreateMap<ItemFatura, UpdateItemFaturaDto>();
            CreateMap<ItemFatura, ReadItemFaturaDto>();
        }
    }
}
