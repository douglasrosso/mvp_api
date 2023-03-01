using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvp_api.Models;

public class UC
{
    [Key]
    [Required]
    public uint? Cod_UC { get; set; }

    [Required(ErrorMessage = "O campo Código do Consumidor é obrigatório")]
    public uint? Cod_Consumidor { get; set; }
    [JsonIgnore]
    public virtual Consumidor Consumidor { get; set; }
    [Required]
    public DateTime? DataLigacao { get; set; }

    [Required(ErrorMessage = "O campo status da Unidade Consumidora é obrigatório")]
    public bool Status { get; set; } = true;

    public long Num_Medidor { get; set; }

    [Column(TypeName = "varchar(250)")]
    public string Num_Casa { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "O campo deve ter no minimo 8 caracteres")]
    public string CEP { get; set; }

    [Required(ErrorMessage = "O campo Logradouro da Unidade Consumidora é obrigatório")]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "O campo Bairro da Unidade Consumidora é obrigatório")]
    [Column(TypeName = "varchar(250)")]
    public string Bairro { get; set; }

    [Column(TypeName = "varchar(250)")]
    public string Complemento { get; set; }

    public virtual List<Fatura> Faturas { get; set; }

}