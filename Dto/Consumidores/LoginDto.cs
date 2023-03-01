using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvp_api.Dto.Consumidores;

public class LoginDto
{
    [Required(ErrorMessage = "O campo documento é obrigatório")]
    public string Doc_Consumidor { get; set; }

    [Required(ErrorMessage = "O campo senha é obrigatório")]
    [DataType(DataType.Password)]
    public string Senha { get; set; }
}
