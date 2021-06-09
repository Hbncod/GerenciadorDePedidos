using GerenciadorDePedidos;
using System;
using Xunit;

namespace GerenciadorDePedidosTests
{
    public class PedidoTests
    {
        [Fact]
        public void QuandoRemoverItensDaListaDoPedidoAQuantidadeDeItensDeveSerAlterada()
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
        public void QuandoTentarRemoverUmItemQueNaoExisteNaListaDeItensDoPedidoDeveLancarUmaExcecao()
        {
            //Arranje
            Pedido pedido = new Pedido();
            ItemPedido item = new ItemPedido(5, 5);
            ItemPedido item2 = new ItemPedido(5, 10);

            ItemPedido item3 = new ItemPedido(2, 10);
            pedido.AdicionarItem(item);
            pedido.AdicionarItem(item2);

            //Assert
            var exceptionObtida = Assert.Throws<Exception>(
                //Act
                () => pedido.RemoverItem(item3));
            Assert.Equal(Pedido.ItemNaoEstaNaListaMensagem, exceptionObtida.Message);
        }
        [Theory]
        [InlineData(5, 9.99)]
        public void QuandoAdicionarItensOsMesmosDevemEstarNaListaDePedido(int quantidade, decimal valorUnitario)
        {
            //Arranje
            ItemPedido item = new ItemPedido(quantidade, valorUnitario);
            Pedido pedido = new Pedido();

            //Act
            pedido.AdicionarItem(item);

            //Assert
            Assert.NotNull(pedido.ItensPedido.Find(i => i.Equals(item)));
        }
        
        [Fact]
        public void QuandoAdicionarUmOuMaisItensDeveAlterarOValorTotalDoPedido()
        {
            //Arranje
            Pedido pedido = new Pedido();
            ItemPedido item = new ItemPedido(5, 5);
            ItemPedido item2 = new ItemPedido(5, 10);
            

            //Act
            pedido.AdicionarItem(item);
            pedido.AdicionarItem(item2);

            //Assert
            Assert.Equal(75, pedido.Total);
        }
    }
}
