using GerenciadorDePedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePedidosTests
{
    public partial class Tools
    {
        protected ItemPedido CriarItemPedido(decimal valorUnitario, int quantidade = 1, int? id = null)
        {
            id ??= 1;
            return new ItemPedido(id.Value, quantidade, valorUnitario);
        }
    }
}
