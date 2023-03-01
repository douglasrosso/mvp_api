using System;

namespace mvp_api.Dto.Item;

public class ReadItensDto
{
    public uint cod_item { get; set; }

    public string nome_item { get; set; }

    public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
}
