using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using mvp_api.Models;
using IndexAttribute = System.ComponentModel.DataAnnotations.Schema.IndexAttribute;

namespace mvp_api.Dto.Consumidores;

public class UpdateDistribuidoraConsumidorDto
{
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