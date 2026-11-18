using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace confeitaria_api.Models;

public partial class Produto
{
    public int ProdutoId { get; set; }

    public int CategoriaId { get; set; }

    public string ProdutoNome { get; set; } = null!;

    public string? ProdutoDescricao { get; set; }

    public decimal ProdutoPreco { get; set; }

    public int ProdutoEstoque { get; set; }

    [JsonIgnore]
    public virtual Categoria Categoria { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
}
