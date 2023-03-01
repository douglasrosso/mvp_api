using mvp_api.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvp_api.Dto.Faturas;

public class FaturaPDFDto
{
    public string Nome_Consumidor { get; set; }

    public string Doc_Consumidor { get; set; }

    public string Telefone1 { get; set; }

    public string Num_casa { get; set; }

    public string Cep { get; set; }

    public string Logradouro { get; set; }

    public string Bairro { get; set; }

    public string Complemento { get; set; }

    public DateOnly? Competencia { get; set; }

    public DateTime? DataEmissao { get; set; }

    public decimal ValorTotalFatura { get; set; }
}
