using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mvp_api.Dto.ItensFatura;

public class CreateItemFaturaDto
{
    
    [Required]
    public uint? Cod_Item { get; set; }

    public long Cod_Fatura { get; set; } 

    [Required(ErrorMessage = "O campo quantidade de item é obrigatório")]
    [Column(TypeName = "decimal(10, 6)")]
    public decimal QuantItem { get; set; }

    [Required(ErrorMessage = "O campo valor do item de consumo é obrigatório")]
    [Column(TypeName = "decimal(10, 6)")]
    public decimal ValorItem { get; set; }
}
