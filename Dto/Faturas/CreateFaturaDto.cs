using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using mvp_api.Models;
using mvp_api.Dto.ItensFatura;

namespace mvp_api.Dto.Faturas;

public class CreateFaturaDto
{
    [Required(ErrorMessage = "O campo código do consumidor é obrigatório")]
    public uint? Cod_consumidor { get; set; }

    [Required(ErrorMessage = "O campo código da unidade consumidora é obrigatório")]
    public uint? Cod_uc { get; set; }

    [Required(ErrorMessage = "O campo competência é obrigatório")]
    public DateOnly Competencia { get; set; }

    [Required(ErrorMessage = "O campo data de emissao é obrigatório")]
    public DateTime? DataEmissao { get; set; } = DateTime.Now.Date;

    [Required]
    [Column(TypeName = "decimal(10,2")]
    public decimal ValorTotalFatura { get; set; }

    [Required(ErrorMessage  = "O campo situação é obrigatório")]
    public bool Pagamento { get; set; }

    public DateTime? DataPagamento { get; set; } = new DateTime(1990, 01, 01);

    public List<ItemFatura> ItemFatura { get; set; }
}
