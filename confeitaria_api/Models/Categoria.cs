using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace confeitaria_api.Models;

public partial class Categoria
{
    public int CategoriaId { get; set; }

    public string CategoriaNome { get; set; } = null!;

    public string? CategoriaDescricao { get; set; }

    [JsonIgnore]
    public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
