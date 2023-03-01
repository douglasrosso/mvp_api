﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using mvp_api.Models;
using Newtonsoft.Json;

namespace mvp_api.Dto.Consumidores;

public class ReadConsumidorDto
{
    [Required]
    public uint? Cod_Consumidor { get; set; }

    public bool Status { get; set; }

    public string Nome_Consumidor { get; set; }

    public string Email { get; set; }

    public string Doc_consumidor { get; set; }

    public string Senha { get; set; }
    [Required]
    public DateTime? Data_cadastro { get; set; }

    public string Telefone1 { get; set; }

    public string Telefone2 { get; set; }
}
