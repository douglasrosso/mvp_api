using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvp_api.Models;

public class Item
{
    [Key]
    [Required]
    public uint? cod_item { get; set; }

    [Required(ErrorMessage = "O campo nome do item é obrigatório")]
    [Column(TypeName = "varchar(250)")]
    public string nome_item { get; set; }

    [JsonIgnore]
    public virtual List<ItemFatura> ItemFaturas { get; set; }
}
