using AutoMapper;
using mvp_api.Dto.UCs;
using mvp_api.Models;

namespace mvp_api.Profiles;

public class UCProfile : Profile
{
    public UCProfile()
    {
        CreateMap<UpdateUCDto, UC>();
        CreateMap<UC, UpdateUCDto>();
        CreateMap<UpdateStatusUCDto, UC>();
        CreateMap<UpdateTitularidadeUCDto, UC>();
        CreateMap<CreateUCDto, UC>();
        CreateMap<UC, ReadUCDto>();
        CreateMap<UC, ReadUCPorCodDto>();
        CreateMap<UC, ReadFaturasPorUCDto>();
    }
}
