using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace confeitaria_api.Models;

public partial class Pedido
{
    public int PedidoId { get; set; }

    public int ClienteId { get; set; }

    public DateTime PedidoData { get; set; }

    public string PedidoStatus { get; set; } = null!;

    [JsonIgnore]
    public virtual Cliente Cliente { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
}
