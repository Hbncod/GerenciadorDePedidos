using GerenciadorDePedidos;
using Xunit;
using System;
using FluentAssertions;

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
            var item = new ItemPedido(1, quantidade, valorUnitario);

            // Assert
            item.Should().NotBeNull();
            item.Total.Should().Equals(valorEsperado);
            //Assert.Equal(valorEsperado, item.Total);
        }

        [Theory]
        [InlineData(9.99, 5, 10)]
        public void QuandoAtualizarQuantidadeDeUmItemDeveCalcularNovoValorTotal(decimal precoUnitario, int quantidade, int novaQuantidade)
        {
            // Arrange
            var item = new ItemPedido(1, quantidade, precoUnitario);           
            var totalEsperado = precoUnitario * novaQuantidade;

            // Act
            item.AtualizarQuantidade(novaQuantidade);

            // Assert
            item.Quantidade.Should().BePositive().Equals(novaQuantidade);
            item.Total.Should().BePositive("Não pode ter um valor negativo").Equals(totalEsperado);
            //Assert.Equal(novaQuantidade, item.Quantidade);
            //Assert.Equal(totalEsperado, item.Total);
        }

        [Theory]
        [InlineData(9.99, 5)]
        public void QuandoAtualizarQuantidadeExcendoOLimiteDeveRetornarUmaExcecao(decimal precoUnitario, int quantidade)
        {
            // Arrange
            var item = new ItemPedido(1, quantidade, precoUnitario);
            var novaQuantidade = 100;

            // Act
            Action action = () => item.AtualizarQuantidade(novaQuantidade);
            action.Should().ThrowExactly<Exception>().WithMessage(ItemPedido.QuantidadeExcedidaMensagem);
            //var exception = Assert.Throws<Exception>(() => item.AtualizarQuantidade(100));

            // Assert
            //Assert.IsType<Exception>(exception);
            //Assert.Equal(ItemPedido.QuantidadeExcedidaMensagem, exception.Message);   
        }
        [Theory]
        [InlineData(100)]
        public void QuandoCriarItemComQuantidadeExcendoOLimiteDeveRetornarUmaExcecao(int quantidade)
        {
            //Act
            //var exception = Assert.Throws<Exception>(() => new ItemPedido(1, quantidade, 10m));

            //Act
            Action action = () => new ItemPedido(1, quantidade, 10m);

            //Assert
            action.Should().NotBeNull().And.ThrowExactly<Exception>("Quando criar item com quantidade excedendo o limite deve lançar uma exceção").WithMessage(ItemPedido.QuantidadeExcedidaMensagem);
            // Assert
            //Assert.NotNull(exception);
            //Assert.IsType<Exception>(exception);
            //Assert.Equal(ItemPedido.QuantidadeExcedidaMensagem, exception.Message);
        }
    }
}
