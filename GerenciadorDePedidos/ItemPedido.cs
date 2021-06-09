using System;

namespace GerenciadorDePedidos
{
    public class ItemPedido
    {
        public const string QuantidadeExcedidaMensagem = "A quantidade não pode ser maior que o permitido";

        private static int contador = 0;

        private const int QTD_MAXIMA_PERMITIDA = 99;

        private int _quantidade;

        public int Id { get; private set; }
        public int Quantidade
        {
            get => _quantidade;
            set
            {
                if (value > QTD_MAXIMA_PERMITIDA)
                    throw new Exception(QuantidadeExcedidaMensagem);

                _quantidade = value;
            }
        }
        public decimal ValorUnitario { get; private set; }

        public decimal Total => Quantidade * ValorUnitario;

        public ItemPedido(int quantidade, decimal valorUnitario)
        {
            Id = contador++;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            //Total = quantidade * valorUnitario;
        }
        public void AtualizarQuantidade(int novaQuantidade)
        {
            Quantidade = novaQuantidade;
            // AtualizaTotal()    
            //Total = 2;
        }
    }
}
