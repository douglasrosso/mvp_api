using AutoMapper;
using mvp_api.Dto.Consumidores;
using mvp_api.Models;

namespace mvp_api.Profiles;

public class ConsumidorProfile : Profile
{
    public ConsumidorProfile()
    {
        CreateMap<CreateConsumidorDto, Consumidor>();
        CreateMap<UpdateDistribuidoraConsumidorDto, Consumidor>();
        CreateMap<UpdateConsumidorDto, Consumidor>();
        CreateMap<Consumidor, UpdateConsumidorDto>();
        CreateMap<Consumidor, ReadConsumidorDto>();
        CreateMap<Consumidor, ReadConsumidorPorCodDto>()
            .ForMember(consumidor => consumidor.Faturas, opts => opts
            .MapFrom(consumidor => consumidor.Faturas.Select
            (f => new { f.Cod_Fatura, f.Cod_Consumidor, f.Cod_UC, f.Competencia, f.DataEmissao, f.ValorTotalFatura, f.Pagamento, f.DataPagamento })));
        CreateMap<Consumidor, LoginDto>();
        CreateMap<CadastroClienteDto, Consumidor>();

    }


}
