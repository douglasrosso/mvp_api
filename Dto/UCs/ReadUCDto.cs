using Microsoft.EntityFrameworkCore.Metadata.Internal;
using mvp_api.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvp_api.Dto.UCs;

public class ReadUCDto
{
    public uint? Cod_uc { get; set; }
    public uint? Cod_consumidor { get; set; }

    public bool Status { get; set; }

    public long Num_medidor { get; set; }

    public string Num_casa { get; set; }

    public string Cep { get; set; }

    public string Logradouro { get; set; }

    public string Bairro { get; set; }

    public string Complemento { get; set; }

    public DateTime DataLigacao { get; set; }
}
