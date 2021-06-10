using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Listas C#

namespace GerenciadorDePedidos
{
    public class Pedido
    {
        private static int contador = 0;
        public const string ItemNaoEstaNaListaMensagem = "Não é possível remover este item, pois ele não existe na lista de itens do pedido";
        public int Id { get; } = contador++;
        public DateTime DataPedido { get; } = DateTime.Now;
        public List<ItemPedido> ItensPedido { get; private set; } = new List<ItemPedido>();
        public decimal Total => ItensPedido.Sum(item => item.Total);
        public PedidoStatusEnum PedidoStatus { get; private set; } = PedidoStatusEnum.Novo;

        //public double Total
        //{
        //    get
        //    {
        //        var total = 0d;

        //        foreach(var item in ItensPedido)
        //        {
        //            total += item.Total;
        //        }

        //        return total;
        //    }
        //}

        public void AdicionarItem(ItemPedido item)
        {
            ItensPedido.Add(item);
        }
        public void RemoverItem(ItemPedido item)
        {
            if(!ItemEstaNaLista(item))
            {
                throw new Exception(ItemNaoEstaNaListaMensagem);
            }
            ItensPedido.Remove(item);
        }
        private bool ItemEstaNaLista(ItemPedido item)
        {
            return null != ItensPedido.
                DefaultIfEmpty()
                .FirstOrDefault(i => i == item);
        }
        public void Cancelar()
        {
            PedidoStatus = PedidoStatusEnum.Cancelado;
        }
    }
}
