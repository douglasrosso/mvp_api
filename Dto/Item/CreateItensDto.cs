using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mvp_api.Dto.Item;

public class CreateItensDto
{
    [Required(ErrorMessage = "O campo nome do item é obrigatório")]
    [Column(TypeName = "varchar(250)")]
    public string nome_item { get; set; }
}
