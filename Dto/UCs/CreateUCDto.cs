using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mvp_api.Dto.UCs;

public class CreateUCDto
{
    [Required(ErrorMessage = "O campo Código do Consumidor é obrigatório")]
    public uint? Cod_consumidor { get; set; }

    [Required(ErrorMessage = "O campo status da Unidade Consumidora é obrigatório")]
    public bool Status { get; set; } = true;

    public long Num_medidor { get; set; }

    [Column(TypeName = "varchar(250)")]
    public string Num_casa { get; set; }

    [Required]
    [StringLength(8)]
    public string Cep { get; set; }

    [Required(ErrorMessage = "O campo Logradouro da Unidade Consumidora é obrigatório")]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "O campo Bairro da Unidade Consumidora é obrigatório")]
    [Column(TypeName = "varchar(250)")]
    public string Bairro { get; set; }

    [Column(TypeName = "varchar(250)")]
    public string Complemento { get; set; }

    public DateTime DataLigacao { get; set; } = DateTime.Now.Date;
    
}
