using mvp_api.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvp_api.Dto.Faturas;

public class ReadFaturaPorCodDto
{
    public long Cod_Fatura { get; set; }

    public uint? Cod_Consumidor { get; set; }

    public uint? Cod_UC { get; set; }

    public DateOnly? Competencia { get; set; }

    public DateTime? DataEmissao { get; set; }

    public decimal ValorTotalFatura { get; set; }

    public bool Pagamento { get; set; }

    public DateTime? DataPagamento { get; set; }

    [JsonIgnore]
    public Consumidor  Consumidor { get; set; }

    public List<ItemFatura> ItemFatura { get; set; }
}
