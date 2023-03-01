using AutoMapper;
using mvp_api.Dto.Consumidores;
using mvp_api.Dto.Faturas;
using mvp_api.Dto.UCs;
using mvp_api.Models;

namespace mvp_api.Profiles
{
    public class FaturaProfile : Profile
    {
        public FaturaProfile()
        {
            CreateMap<UpdateFaturaDto, Fatura>();
            CreateMap<Fatura, UpdateFaturaDto>();
            CreateMap<CreateFaturaDto, Fatura>();
            CreateMap<Fatura, ReadFaturaDto>();
        }

    }
}
