using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using mvp_api.Models;

namespace mvp_api.Dto.Faturas;

public class UpdateFaturaDto
{
        [Required(ErrorMessage = "O campo código do consumidor é obrigatório")]
    public uint? Cod_consumidor { get; set; }

    [Required(ErrorMessage = "O campo código da unidade consumidora é obrigatório")]
    public uint? Cod_uc { get; set; }

    [Required(ErrorMessage = "O campo competência é obrigatório")]
    public DateOnly? Competencia { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2")]
    public decimal ValorTotalFatura { get; set; }

    [Required(ErrorMessage = "O campo situação é obrigatório")]
    public bool Pagamento { get; set; }

    public DateTime DataPagamento { get; set; }

    public List<ItemFatura> ItemFatura { get; set; }
}