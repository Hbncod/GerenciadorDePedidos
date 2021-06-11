using System;
using System.Collections.Generic;
using System.Linq;

// Listas C#

namespace GerenciadorDePedidos
{
    public class Pedido
    {
        public Pedido(PedidoStatus status /*= PedidoStatus.Cancelado*/)
        {
            Status = status;
        }
        public Pedido()
        {
        }

        private List<ItemPedido> _items = new();

        private static int contador = 0;
        public const string ItemNaoEstaNaListaMensagem = "Não é possível remover este item, pois ele não existe na lista de itens do pedido";
        public const string ItemNaoPodeSerAdicionadoSeStatusEstaCanceladoOuPagoMensagem = "Não é possível alterar a lista de pedidos, pois ela foi cancelada ou paga";
        public const string PedidoJaCanceladoMensagem = "Não é possível Cancelar este pedido, pois ele já foi cancelado";
        public int Id { get; } = contador++;
        public DateTime DataPedido { get; } = DateTime.Now;
        public IReadOnlyCollection<ItemPedido> ItensPedido => _items.AsReadOnly();

        public decimal Total => ItensPedido.Sum(item => item.Total);
        public PedidoStatus Status { get; private set; } = PedidoStatus.Novo;
        public FaturaPedido Fatura { get; private set; }

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
            // Eu não posso adicionar itens para um pedido que foi cancelado ou pago/finalizado.
            // O Seu arrange (estado inicial do pedido) para o teste deve iniciar com o pedido cancelado (neste caso).
            // Regra de negócio aqui ...
            PedidoCanceladoOuPago();

            _items.Add(item);

            Status = PedidoStatus.EmAndamento;
        }

        private void PedidoCanceladoOuPago()
        {
            if (Status == PedidoStatus.Cancelado || Status == PedidoStatus.Pago)
                throw new Exception(ItemNaoPodeSerAdicionadoSeStatusEstaCanceladoOuPagoMensagem);
        }

        public void RemoverItem(ItemPedido item)
        {
            // Eu não posso remover itens para um pedido que foi cancelado ou pago/finalizado. Neste caso só posso se estiver em andamento (já tenho um item)

            if (!_items.Any(x => x.Id == item.Id))
            {
                throw new Exception(ItemNaoEstaNaListaMensagem);
            }

            _items.Remove(item);
        }
        public void Cancelar()
        {
            // Não posso cancelar um pedido se já estiver cancelado.
            if (Status == PedidoStatus.Cancelado)
                throw new Exception(PedidoJaCanceladoMensagem);

            Status = PedidoStatus.Cancelado;
        }

        public void Pagar()
        {
            // Não posso pagar um pedido cancelado ou já pago.
            PedidoCanceladoOuPago();

            Status = PedidoStatus.Pago;
            Fatura = new FaturaPedido();
        }
    }
}
