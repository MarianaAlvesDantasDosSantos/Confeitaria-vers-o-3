using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace confeitaria_api.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string ClienteNome { get; set; } = null!;

    public string ClienteEmail { get; set; } = null!;

    public string ClienteCpf { get; set; } = null!;

    public string? ClienteTelefone { get; set; }

    public DateTime ClienteDataCadastro { get; set; }

    [JsonIgnore]
    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
