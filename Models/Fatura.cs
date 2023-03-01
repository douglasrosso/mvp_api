using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvp_api.Models;

public class Fatura
{
    [Key]
    [Required]
    public long Cod_Fatura { get; set; }

    [Required(ErrorMessage = "O campo código do consumidor é obrigatório")]
    public uint? Cod_Consumidor { get; set; }
    
    [Required(ErrorMessage = "O campo código da unidade consumidora é obrigatório")]
    public uint? Cod_UC { get; set; }

    [Required(ErrorMessage = "O campo competência é obrigatório")]
    public DateOnly? Competencia { get; set; }

    [Required(ErrorMessage = "O campo data de emissao é obrigatório")]
    public DateTime? DataEmissao { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal ValorTotalFatura { get; set; }

    [Required(ErrorMessage = "O campo situação é obrigatório")]
    public bool Pagamento { get; set; }

    public DateTime? DataPagamento { get; set; } =
    new DateTime(1990, 01, 01);

    [JsonIgnore]
    public virtual Consumidor Consumidor { get; set; }

    [JsonIgnore]
    public virtual UC UC { get; set; }

    public virtual List<ItemFatura> ItemFatura { get; set; }
}
