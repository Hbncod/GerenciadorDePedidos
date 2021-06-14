using FluentAssertions;
using GerenciadorDePedidos;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ItemPedido item = new ItemPedido(1, 5, 5);
            ItemPedido item2 = new ItemPedido(1, 5, 10);
            ItemPedido item3 = new ItemPedido(1, 2, 3);

            pedido.AdicionarItem(item);
            pedido.AdicionarItem(item2);
            pedido.AdicionarItem(item3);

            //Act
            pedido.RemoverItem(item);

            //Assert
            pedido.ItensPedido.Should().HaveCount(2, "Quando Remover Itens da lista a quantidade deve ser alterada");
            //Assert.Equal(2, pedido.ItensPedido.Count);
        }
        [Fact]
        public void QuandoTentarCancelarUmPedidoJaCanceladoDeveLancarUmaExcecao()
        {
            //Arranje
            Pedido pedido = new Pedido(PedidoStatus.Cancelado);

            //Act
            Action action = () => pedido.Cancelar();

            //Assert
            action.Should().ThrowExactly<Exception>("Quando tentar cancelar um pedido já cancelado, deve lançar uma exceção").WithMessage(Pedido.PedidoJaCanceladoMensagem);
            //var exceptionObtida = Assert.Throws<Exception>(() => pedido.Cancelar());

            //Assert
            //Assert.IsType<Exception>(exceptionObtida);
            //Assert.Equal(Pedido.PedidoJaCanceladoMensagem, exceptionObtida.Message);
        }
        [Fact]
        public void QuandoTentarRemoverUmItemQueNaoExisteNaListaDeItensDoPedidoDeveLancarUmaExcecao()
        {
            //Arranje
            Pedido pedido = new Pedido();
            var item = new ItemPedido(1, 2, 10);


            //Act
            Action action = () => pedido.RemoverItem(item);

            //Assert
            action.Should().ThrowExactly<Exception>("Quando tentar remover um item que não existe na lista de itens, deve lançar uma exceção").WithMessage(Pedido.ItemNaoEstaNaListaMensagem);
            //var exceptionObtida = Assert.Throws<Exception>(() => pedido.RemoverItem(item));

            ////Assert
            //Assert.IsType<Exception>(exceptionObtida);
            //Assert.Equal(Pedido.ItemNaoEstaNaListaMensagem, exceptionObtida.Message);
        }
        [Theory]
        [InlineData(5, 9.99)]
        public void QuandoAdicionarItensOsMesmosDevemEstarNaListaDePedido(int quantidade, decimal valorUnitario)
        {
            //Arranje
            ItemPedido item = new ItemPedido(1, quantidade, valorUnitario);
            Pedido pedido = new Pedido();

            //Act
            pedido.AdicionarItem(item);

            //Assert
            pedido.ItensPedido.Should().NotBeNull().And.HaveCount(1, "Quando adicionar itens a um pedido, o número de itens deve aumentar");
            pedido.ItensPedido.FirstOrDefault(x => x.Id == item.Id).Should().NotBeNull().And.BeEquivalentTo<ItemPedido>(item, "O item encontrado deve ser o mesmo que foi adiocionado");
            //var itemAdicionado = pedido.ItensPedido.FirstOrDefault(x => x.Id == item.Id);
            //Assert.Equal(item, itemAdicionado);
        }

        [Fact]
        public void QuandoAdicionarUmOuMaisItensDeveAlterarOValorTotalDoPedido()
        {
            //Arranje
            Pedido pedido = new Pedido();
            ItemPedido item = new ItemPedido(1, 5, 5);
            ItemPedido item2 = new ItemPedido(1, 5, 10);


            //Act
            pedido.AdicionarItem(item);
            pedido.AdicionarItem(item2);

            //Assert
            pedido.Total.Should().Equals(75);
            //Assert.Equal(75, pedido.Total);
        }
        [Fact]
        public void QuandoCriarUmPedidoSeuStatusDeveEstarComoNovo()
        {
            //Arrange Act
            Pedido pedido = new Pedido();
            //Assert
            pedido.Status.Should().Equals(PedidoStatus.Novo);
            //Assert.Equal(PedidoStatus.Novo, pedido.Status);
        }
        [Fact]
        public void QuandoAdicionarUmItemAUmPedidoComStatusCanceladoDeveLancarUmaExcecao()
        {
            //Arrange
            var pedido = new Pedido(PedidoStatus.Cancelado);
            var item = new ItemPedido(1, 2, 2);

            //Act
            Action action = () => pedido.AdicionarItem(item);

            //Assert
            action.Should().ThrowExactly<Exception>().WithMessage(Pedido.ItemNaoPodeSerAdicionadoSeStatusEstaCanceladoOuPagoMensagem);
            //var excecao = Assert.Throws<Exception>(() => pedido.AdicionarItem(item));

            //Assert.IsType<Exception>(excecao);
            //Assert.Equal(Pedido.ItemNaoPodeSerAdicionadoSeStatusEstaCanceladoOuPagoMensagem, excecao.Message);
                        
        }
        [Fact]
        public void QuandoAdicionarUmItemAUmPedidoComStatusPagoDeveLancarUmaExcecao()
        {
            //Arrange
            var pedido = new Pedido(PedidoStatus.Pago);
            var item = new ItemPedido(1, 2, 2);

            //Act
            Action action = () => pedido.AdicionarItem(item);

            //Assert
            action.Should().ThrowExactly<Exception>().WithMessage(Pedido.ItemNaoPodeSerAdicionadoSeStatusEstaCanceladoOuPagoMensagem);

            //var excecao = Assert.Throws<Exception>(() => pedido.AdicionarItem(item));

            //Assert.IsType<Exception>(excecao);
            //Assert.Equal(Pedido.ItemNaoPodeSerAdicionadoSeStatusEstaCanceladoOuPagoMensagem, excecao.Message);
        }
        [Fact]
        public void QuandoCancelarUmPedidoSeuStatusDeveSerAlteradoParaCancelado()
        {
            //Arrange
            Pedido pedido = new Pedido();
            //Act
            pedido.Cancelar();
            //Assert
            pedido.Status.Should().Equals(PedidoStatus.Cancelado);
            //Assert.Equal(PedidoStatus.Cancelado, pedido.Status);
        }
        [Fact]
        public void QuandoItensOPedidoStatusDeveSerAlteradoParaEmAndamento()
        {
            //Arrange
            Pedido pedido = new Pedido();
            var item = new ItemPedido(1, 2, 10.50m);

            //Act
            pedido.AdicionarItem(item);

            //Assert
            pedido.Status.Should().Equals(PedidoStatus.EmAndamento);
            //Assert.Equal(PedidoStatus.EmAndamento, pedido.Status);
        }
        [Fact]
        public void QuandoRealizarPagamentoOPedidoStatusDeveSerAlteradoParaPago()
        {
            //Arrange
            Pedido pedido = new Pedido();
            var item = new ItemPedido(1, 2, 10.50m);
            pedido.AdicionarItem(item);

            //Act
            pedido.Pagar();

            //Assert
            pedido.Status.Should().Equals(PedidoStatus.Pago);
            //Assert.Equal(PedidoStatus.Pago, pedido.Status);
        }
        [Fact]
        public void QuandoRealizarPagamentoDeveSerGeradoUmaFatura()
        {
            //Arrange
            Pedido pedido = new Pedido();
            var item = new ItemPedido(1, 2, 10.50m);
            pedido.AdicionarItem(item);

            //Act
            pedido.Pagar();

            //Assert
            pedido.Fatura.Should().NotBeNull().And.BeOfType<FaturaPedido>("Quando realizar pagamento deve ser gerada uma fatura");
            //Assert.NotNull(pedido.Fatura);
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
