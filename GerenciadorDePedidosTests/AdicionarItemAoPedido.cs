using GerenciadorDePedidos;
using Xunit;

namespace GerenciadorDePedidosTests
{
    public class AdicionarItemAoPedido
    {
        [Theory]
        [InlineData(5, 9.99)]
        public void ItemAdicionadoDeveEstarNaListaDeItensDoPedido(int quantidade, decimal valorUnitario)
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
        public void ValorTotalDoPedidoDeveSerAlteradoAoAdicionarUmOuMaisItens()
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
