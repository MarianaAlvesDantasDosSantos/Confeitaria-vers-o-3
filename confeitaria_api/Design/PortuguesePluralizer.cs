using Microsoft.EntityFrameworkCore.Design;

namespace confeitaria_api.Design;

public class PortuguesePluralizer : IPluralizer
{
    public string Singularize(string identifier)
    {
        return identifier.ToLower() switch
        {
            "clientes"      => "Cliente",
            "categorias"    => "Categoria",
            "produtos"      => "Produto",
            "pedidos"       => "Pedido",

            // O banco usa "itens_pedido" (singular).
            // O EF pode entregar "ItensPedido" ou "ItensPedidos".
            "itens_pedido"  => "ItemPedido",
            "itenspedido"   => "ItemPedido",
            "itenspedidos"  => "ItemPedido",

            _ => identifier
        };
    }

    public string Pluralize(string identifier)
    {
        return identifier switch
        {
            "Cliente"    => "Clientes",
            "Categoria"  => "Categorias",
            "Produto"    => "Produtos",
            "Pedido"     => "Pedidos",
            "ItemPedido" => "ItensPedido",

            _ => identifier
        };
    }
}