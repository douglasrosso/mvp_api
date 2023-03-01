using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mvp_api.Dto.Consumidores;

public class CadastroClienteDto
{
    [Column(TypeName = "varchar(250)")]
    public string Nome_Consumidor { get; set; }

    [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
    [Column(TypeName = "varchar(250)")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    public string Senha { get; set; }
}
