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

        //public Pedido()
        //{
        //    //Id = contador++;
        //    //DataPedido = DateTime.Now;
        //    //ItensPedido = new List<Item>();
        //}

        public void AdicionarItem(ItemPedido item)
        {
            ItensPedido.Add(item);
            //AtualizarTotal();
        }
        //private void AtualizarTotal()
        //{
        //    Total = 0;
        //    foreach (var item in ItensPedido)
        //    {
        //        Total += item.Total;
        //    }
        //}
        public void RemoverItem(ItemPedido item)
        {
            if(!ItemEstaNaLista(item))
            {
                throw new Exception(ItemNaoEstaNaListaMensagem);
            }
            ItensPedido.Remove(item);
            //AtualizarTotal();
        }
        private bool ItemEstaNaLista(ItemPedido item)
        {
            return null != ItensPedido.
                DefaultIfEmpty()
                .FirstOrDefault(i => i == item);
        }
    }
}
