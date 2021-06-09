using GerenciadorDePedidos;
using Xunit;
using System;

namespace GerenciadorDePedidosTests
{
    public class ItemPedidoTests
    {
        [Theory]
        [InlineData(5, 9.99, 49.95)]
        [InlineData(2, 9.99, 19.98)]
        public void QuandoCriarUmItemDePedidoDeveCalcularValorTotalCorretamente(int quantidade, decimal valorUnitario, decimal valorEsperado)
        {
            //Arranje
            //Act

            var item = new ItemPedido(quantidade, valorUnitario);

            //Assert
            Assert.Equal(valorEsperado, item.Total);
        }

        [Fact]
        public void QuandoAtualizarQuantidadeDeUmItemDeveCalcularNovoValorTotal()
        {
            // Arrange:
            var precoUnitario = 9.99m;
            var item = new ItemPedido(5, precoUnitario);           
            var novaQuantidade = 10;
            var total = precoUnitario * novaQuantidade;

            // Act:

            item.AtualizarQuantidade(novaQuantidade);

            Assert.Equal(novaQuantidade, item.Quantidade);
            Assert.Equal(total, item.Total);
        }

        [Fact]
        public void QuandoAtualizarQuantidadeExcendoOLimiteDeveRetornarUmaExcecao()
        {
            var item = new ItemPedido(4, 10m);

            // Act

            var exception = Assert.Throws<Exception>(() => item.AtualizarQuantidade(100));

            // Assert

            Assert.NotNull(exception);
            Assert.IsType<Exception>(exception);
            Assert.Equal(ItemPedido.QuantidadeExcedidaMensagem, exception.Message);   
        }

        [Fact]
        public void QuandoCriarItemComQuantidadeExcendoOLimiteDeveRetornarUmaExcecao()
        {
            var exception = Assert.Throws<Exception>(() => new ItemPedido(100, 10m));

            // Assert

            Assert.NotNull(exception);
            Assert.IsType<Exception>(exception);
            Assert.Equal(ItemPedido.QuantidadeExcedidaMensagem, exception.Message);
        }
    }
}
