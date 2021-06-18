using GerenciadorDePedidos;
using Xunit;
using System;
using FluentAssertions;

namespace GerenciadorDePedidosTests
{
    public class ItemPedidoTests : Tools
    {

        [Theory]
        [InlineData(5, 9.99)]
        [InlineData(2, 20.99)]
        public void QuandoCriarUmItemDePedidoDeveCalcularValorTotalCorretamente(int quantidade, decimal valorUnitario)
        {
            // Arrange
            var valorEsperado = quantidade * valorUnitario; 

            // Act
            var item = CriarItemPedido(valorUnitario, quantidade);

            // Assert
            item.Should().NotBeNull();
            item.Total.Should().Equals(valorEsperado);
        }


        [Theory]
        [InlineData(9.99, 5, 10)]
        public void QuandoAtualizarQuantidadeDeUmItemDeveCalcularNovoValorTotal(decimal precoUnitario, int quantidade, int novaQuantidade)
        {
            // Arrange
            var item = CriarItemPedido(precoUnitario, quantidade);
            var totalEsperado = precoUnitario * novaQuantidade;

            // Act
            item.AtualizarQuantidade(novaQuantidade);

            // Assert
            item.Quantidade.Should().BePositive().Equals(novaQuantidade);
            item.Total.Should().BePositive("Não pode ter um valor negativo").Equals(totalEsperado);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-20)]
        [InlineData(100)]
        public void QuandoAtualizarAQuantidadeDeUmItemParaUmaQuantidadeInvalidaDeveLancarUmaExcecao(int quantidadeInvalida)
        {
            // Arrange
            var item = CriarItemPedido(5.99m);

            //Act
            Action action = () => item.AtualizarQuantidade(quantidadeInvalida);

            //Assert
            action.Should().ThrowExactly<Exception>("Quando atualizar a quantidade de um item para uma quantidade inválida deve ser lançada uma exceção").WithMessage(ItemPedido.QuantidadeInValidaMensagem);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(100)]
        public void QuandoCriarItemPedidoComQuantidadeInvalida_Deve_LancarUmaExcecao(int quantidadeInvalida)
        {
            //Arrange /Act
            Action action = () => CriarItemPedido(11.99m, quantidadeInvalida);

            //Assert
            action.Should().ThrowExactly<Exception>("Quando criar um pedido com quantidade inválida deve ser lançada uma exceção").WithMessage(ItemPedido.QuantidadeInValidaMensagem);
        }
    }
}
