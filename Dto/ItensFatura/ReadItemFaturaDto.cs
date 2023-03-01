using mvp_api.Models;
using System.ComponentModel.DataAnnotations;

namespace mvp_api.Dto.ItensFatura;

public class ReadItemFaturaDto
{
    public uint Cod_ItemFatura { get; set; }

    public uint Cod_Item { get; set; }

    public long Cod_Fatura { get; set; }

    public decimal QuantItem { get; set; }

    public decimal ValorItem { get; set; }

    public decimal ValorTotal { get; set; }

    public DateTime HoraDaConsulta { get; set; } = DateTime.Now;


}
