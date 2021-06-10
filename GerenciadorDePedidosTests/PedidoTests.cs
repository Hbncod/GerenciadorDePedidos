using GerenciadorDePedidos;
using System;
using System.Collections.Generic;
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
            ItemPedido item3 = new ItemPedido(2, 3);

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
            var item = new ItemPedido(2, 10);


            //Act
            var exceptionObtida = Assert.Throws<Exception>(() => pedido.RemoverItem(item));

            //Assert
            Assert.IsType<Exception>(exceptionObtida);
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
        [Fact]
        public void QuandoCancelarUmPedidoSeuStatusDeveSerAlteradoParaCancelado()
        {
            Pedido pedido = new Pedido();

            pedido.Cancelar();

            Assert.Equal(PedidoStatusEnum.Cancelado, pedido.PedidoStatus);
        }



        //public static IEnumerable<object[]> ItensPedido()
        //{
        //    yield return new ItemPedido[] { new ItemPedido(5, 10), new ItemPedido(2, 20), new ItemPedido(3, 15) };
        //    yield return new ItemPedido[] { new ItemPedido(2, 5.99m), new ItemPedido(2, 15.99m), new ItemPedido(3, 10) };
        //}
        //private void AdicionarItensPedidoEmPedido(Pedido pedido, ItemPedido[] itens)
        //{
        //    foreach (var item in itens)
        //    {
        //        pedido.AdicionarItem(item);
        //    }
        //}
    }
}
