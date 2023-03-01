using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using mvp_api.Models;
using IndexAttribute = System.ComponentModel.DataAnnotations.Schema.IndexAttribute;

namespace mvp_api.Dto.Consumidores;

public class UpdateConsumidorDto
{
    public bool Status { get; set; }

    [Required(ErrorMessage = "O campo nome é obrigatório")]
    [Column(TypeName = "varchar(250)")]
    public string Nome_Consumidor { get; set; }

    [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
    [Column(TypeName = "varchar(250)")]
    [Index("Email", IsUnique = true)]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo documento é obrigatório")]
    [Index("Documento", IsUnique = true)]
    [StringLength(14, MinimumLength = 11, ErrorMessage = "O CPF deve conter deve conter no mínimo 11 caracteres e o CNPJ no mínimo 14 caracteres")]
    public string Doc_Consumidor { get; set; }

    [DataType(DataType.Password)]
    public string Senha { get; set; }

    [Required(ErrorMessage = "O campo telefone 1 é obrigatório")]
    [StringLength(11, MinimumLength = 10, ErrorMessage = "O telefone tem que ter no minimo 10 caracteres" +
        "e no maximo 11")]
    [Column(TypeName = "varchar(11)")]
    public string Telefone1 { get; set; }

    [StringLength(11, MinimumLength = 10, ErrorMessage = "O telefone tem que ter no minimo 10 caracteres" +
       "e no maximo 11")]
    [Column(TypeName = "varchar(11)")]
    public string Telefone2 { get; set; }
}