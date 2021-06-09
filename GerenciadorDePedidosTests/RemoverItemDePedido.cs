using GerenciadorDePedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GerenciadorDePedidosTests
{
    public class RemoverItemDePedido
    {
        [Fact]
        public void QuantidadeDeItensDoPedidoDeveSerAlteradaAoRemoverItens()
        {
            //Arranje
            Pedido pedido = new Pedido();
            ItemPedido item = new ItemPedido(5, 5);
            ItemPedido item2 = new ItemPedido(5, 10);
            ItemPedido item3 = new ItemPedido(2, 10);
            pedido.AdicionarItem(item);
            pedido.AdicionarItem(item2);
            pedido.AdicionarItem(item3);


            //Act
            pedido.RemoverItem(item);

            //Assert
            Assert.Equal(2, pedido.ItensPedido.Count);
        }
        [Fact]
        public void LancaExceptionAoTentarRemoverUmItemQueNaoExiste()
        {
            //Arranje
            Pedido pedido = new Pedido();
            ItemPedido item = new ItemPedido(5, 5);
            ItemPedido item2 = new ItemPedido(5, 10);

            ItemPedido item3 = new ItemPedido(2, 10);
            pedido.AdicionarItem(item);
            pedido.AdicionarItem(item2);
            string messagemEsperada = "Não é possível remover este item, pois ele não existe na lista de itens do pedido";

            //Assert
            var exceptionObtida = Assert.Throws<Exception>(
                //Act
                () => pedido.RemoverItem(item3));
            Assert.Equal(messagemEsperada, exceptionObtida.Message);
        }
        //[Fact]
        //public void LancaExceptionAoTentarRemoverUmItemDeUmaListaQueNaoExiste()
        //{
        //    //Arranje
        //    Pedido pedido = new Pedido();
        //    Item item = new Item(5, 5);
        //    string messagemEsperada = "Não é possível remover este item, pois ele não existe na lista de itens do pedido";

        //    //Assert
        //    var exceptionObtida = Assert.Throws<Exception>(
        //        //Act
        //        () => pedido.RemoverItem(item));
        //    Assert.Equal(messagemEsperada, exceptionObtida.Message);
        //}

    }
}
