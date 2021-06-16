using FluentAssertions;
using GerenciadorDePedidos;
using System;
using System.Linq;
using Xunit;

namespace GerenciadorDePedidosTests
{
    public class PedidoTests : Tools
    {
        [Fact]
        public void QuandoRemoverItensDaListaDoPedidoAQuantidadeDeItensDeveSerAlterada()
        {
            //Arranje
            Pedido pedido = new Pedido();
            var item = CriarItemPedido(1.50m);
            var item2 = CriarItemPedido(1.50m);
            var item3 = CriarItemPedido(1.50m);

            pedido.AdicionarItem(item);
            pedido.AdicionarItem(item2);
            pedido.AdicionarItem(item3);

            //Act
            pedido.RemoverItem(item);

            //Assert
            pedido.ItensPedido.Should().HaveCount(2, "Quando Remover Itens da lista a quantidade deve ser alterada");
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
        }
        [Fact]
        public void QuandoTentarRemoverUmItemQueNaoExisteNaListaDeItensDoPedidoDeveLancarUmaExcecao()
        {
            //Arranje
            var pedido = new Pedido();
            var item = CriarItemPedido(1.30m);


            //Act
            Action action = () => pedido.RemoverItem(item);

            //Assert
            action.Should().ThrowExactly<Exception>("Quando tentar remover um item que não existe na lista de itens, deve lançar uma exceção").WithMessage(Pedido.ItemNaoEstaNaListaMensagem);
        }
        [Theory]
        [InlineData(5, 9.99)]
        [InlineData(2, 1.99)]
        public void QuandoAdicionarItensOsMesmosDevemEstarNaListaDePedido(int quantidade, decimal valorUnitario)
        {
            //Arranje
            var item = CriarItemPedido(valorUnitario, quantidade);
            var pedido = new Pedido();
            var qntAntigaDeItens = pedido.ItensPedido.Count;

            //Act
            pedido.AdicionarItem(item);

            //Assert
            pedido.ItensPedido.Should().NotBeNull().And.HaveCount(c => c > qntAntigaDeItens, "Quando adicionar itens a um pedido, o número de itens deve aumentar");
            pedido.ItensPedido.FirstOrDefault(x => x.Id == item.Id).Should().NotBeNull().And.BeEquivalentTo<ItemPedido>(item, "O item encontrado deve ser o mesmo que foi adiocionado");
        }

        [Fact]
        public void QuandoAdicionarUmOuMaisItensDeveAlterarOValorTotalDoPedido()
        {
            //Arranje
            var pedido = new Pedido();
            var item = CriarItemPedido(10m);
            var item2 = CriarItemPedido(15.30m);
            var totalEsperado = item.Total + item2.Total;


            //Act
            pedido.AdicionarItem(item);
            pedido.AdicionarItem(item2);

            //Assert
            pedido.Total.Should().Equals(totalEsperado);
        }
        [Fact]
        public void QuandoCriarUmPedidoSeuStatusDeveEstarComoNovo()
        {
            //Arrange Act
            Pedido pedido = new Pedido();
            //Assert
            pedido.Status.Should().HaveFlag(PedidoStatus.Novo);
        }
        [Theory]
        [InlineData(PedidoStatus.Cancelado)]
        [InlineData(PedidoStatus.Pago)]
        public void QuandoAdicionarUmItemAUmPedidoComStatusDiferenteDeNovoOuEmAndamentoDeveLancarUmaExcecao(PedidoStatus status)
        {
            //Arrange
            var pedido = new Pedido(status);
            var item = CriarItemPedido(10);

            //Act
            Action action = () => pedido.AdicionarItem(item);

            //Assert
            action.Should().ThrowExactly<Exception>().WithMessage(Pedido.ItemNaoPodeSerAdicionadoSeStatusEstaCanceladoOuPagoMensagem);
        }
        [Fact]
        public void QuandoCancelarUmPedidoSeuStatusDeveSerAlteradoParaCancelado()
        {
            //Arrange
            Pedido pedido = new Pedido();
            //Act
            pedido.Cancelar();
            //Assert
            pedido.Status.Should().HaveFlag(PedidoStatus.Cancelado);
        }
        [Fact]
        public void QuandoAdicionarItensaOPedidoOPedidoStatusDeveSerAlteradoParaEmAndamento()
        {
            //Arrange
            Pedido pedido = new Pedido();
            var item = CriarItemPedido(10.20m);

            //Act
            pedido.AdicionarItem(item);

            //Assert
            pedido.Status.Should().HaveFlag(PedidoStatus.EmAndamento);
        }
        [Fact]
        public void QuandoRealizarPagamentoOPedidoStatusDeveSerAlteradoParaPago()
        {
            //Arrange
            Pedido pedido = new Pedido();
            var item = CriarItemPedido(10.20m);
            pedido.AdicionarItem(item);

            //Act
            pedido.Pagar();

            //Assert
            pedido.Status.Should().HaveFlag(PedidoStatus.Pago);
        }
        [Fact]
        public void QuandoRealizarPagamentoDeveSerGeradoUmaFatura()
        {
            //Arrange
            Pedido pedido = new Pedido();
            var item = CriarItemPedido(10.20m);
            pedido.AdicionarItem(item);

            //Act
            pedido.Pagar();

            //Assert
            pedido.Fatura.Should().NotBeNull().And.BeOfType<FaturaPedido>("Quando realizar pagamento deve ser gerada uma fatura");
        }
    }
}
