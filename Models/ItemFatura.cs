using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvp_api.Models;

public class ItemFatura
{
    [Key]
    [Required]
    public uint Cod_ItemFatura { get; set; }

    [Required]
    public uint? Cod_Item { get; set; }

    public long Cod_Fatura { get; set; }

    [Required(ErrorMessage = "O campo quantidade de item é obrigatório")]
    [Column(TypeName = "decimal(10, 6)")]
    public decimal QuantItem { get; set; }

    [Required(ErrorMessage = "O campo valor do item de consumo é obrigatório")]
    [Column(TypeName = "decimal(10, 6)")]
    public decimal ValorItem { get; set; }

    [Column(TypeName = "decimal(10, 6)")]
    public decimal ValorTotal
    {
        get { return QuantItem * ValorItem; }
        set { }
    }

    [JsonIgnore]
    public virtual Item Item { get; set; }

    [JsonIgnore]
    public virtual Fatura Fatura { get; set; }
}
