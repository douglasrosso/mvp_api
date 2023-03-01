using mvp_api.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvp_api.Dto.Faturas;

public class ReadFaturaDto
{
    public long Cod_Fatura { get; set; }

    [Required]
    public uint? Cod_Consumidor { get; set; }

    [Required]
    public uint? Cod_UC { get; set; }

    [Required]
    public DateOnly? Competencia { get; set; }

    [Required]
    public DateTime? DataEmissao { get; set; }

    public decimal ValorTotalFatura { get; set; }

    public bool Pagamento { get; set; }

    [Required]
    public DateTime? DataPagamento { get; set; }
}
