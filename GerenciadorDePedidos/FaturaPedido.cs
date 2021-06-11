using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePedidos
{
    public class FaturaPedido
    {
        private static int contador = 0;
        public int Id { get; } = contador++;
        public DateTime DataDocumento { get; } = DateTime.Now;
        public DateTime DataVencimento { get; } = DateTime.Now.AddDays(10);

    }
}
