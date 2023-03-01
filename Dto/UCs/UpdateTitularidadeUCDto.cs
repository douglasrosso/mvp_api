using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvp_api.Dto.UCs;

public class UpdateTitularidadeUCDto
{
    [Required]
    public uint? Cod_consumidor { get; set; }

    public DateTime DataLigacao { get; set; }
}
