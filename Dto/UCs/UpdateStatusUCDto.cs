using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvp_api.Dto.UCs;

public class UpdateStatusUCDto
{
    [Required(ErrorMessage = "O campo status da Unidade Consumidora é obrigatório")]
    public bool Status { get; set; }

    public DateTime DataLigacao { get; set; }
}
