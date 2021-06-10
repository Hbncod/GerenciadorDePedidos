using GerenciadorDePedidos;
using Xunit;
using System;

namespace GerenciadorDePedidosTests
{
    public class ItemPedidoTests
    {
        [Theory]
        [InlineData(5, 9.99)]
        [InlineData(2, 20.99)]
        public void QuandoCriarUmItemDePedidoDeveCalcularValorTotalCorretamente(int quantidade, decimal valorUnitario)
        {
            // Arrange
            var valorEsperado = quantidade * valorUnitario; 

            // Act
            var item = new ItemPedido(quantidade, valorUnitario);

            // Assert
            Assert.Equal(valorEsperado, item.Total);
        }

        [Theory]
        [InlineData(9.99, 5, 10)]
        public void QuandoAtualizarQuantidadeDeUmItemDeveCalcularNovoValorTotal(decimal precoUnitario, int quantidade, int novaQuantidade)
        {
            // Arrange:
            var item = new ItemPedido(quantidade, precoUnitario);           
            var totalEsperado = precoUnitario * novaQuantidade;

            // Act:
            item.AtualizarQuantidade(novaQuantidade);

            // Assert
            Assert.Equal(novaQuantidade, item.Quantidade);
            Assert.Equal(totalEsperado, item.Total);
        }

        [Theory]
        [InlineData(9.99, 5)]
        public void QuandoAtualizarQuantidadeExcendoOLimiteDeveRetornarUmaExcecao(decimal precoUnitario, int quantidade)
        {
            // Arrange
            var item = new ItemPedido(quantidade, precoUnitario);

            // Act
            var exception = Assert.Throws<Exception>(() => item.AtualizarQuantidade(100));

            // Assert

            Assert.NotNull(exception);
            Assert.IsType<Exception>(exception);
            Assert.Equal(ItemPedido.QuantidadeExcedidaMensagem, exception.Message);   
        }

        [Theory]
        [InlineData(100)]
        public void QuandoCriarItemComQuantidadeExcendoOLimiteDeveRetornarUmaExcecao(int quantidade)
        {
            var exception = Assert.Throws<Exception>(() => new ItemPedido(quantidade, 10m));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<Exception>(exception);
            Assert.Equal(ItemPedido.QuantidadeExcedidaMensagem, exception.Message);
        }
    }
}
