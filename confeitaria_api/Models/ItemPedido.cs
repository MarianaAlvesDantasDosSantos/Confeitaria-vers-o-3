using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace confeitaria_api.Models;

public partial class ItemPedido
{
    public int PedidoId { get; set; }

    public int ProdutoId { get; set; }

    public int ItemPedidoQuantidade { get; set; }

    public decimal ItemPedidoPrecoUnitario { get; set; }

    [JsonIgnore]
    public virtual Pedido Pedido { get; set; } = null!;

    [JsonIgnore]
    public virtual Produto Produto { get; set; } = null!;
}
