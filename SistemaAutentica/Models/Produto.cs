using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAutentica.Models
{
    public class Produto
    {

        public int ProdutoID { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public float Custo { get; set; }
        public string Unidade { get; set; }
        public string NumeroNotaFiscal { get; set; }
        public float Total { get; set; }
        public int NotaID { get; set; }


        public float getTotal()
        {
            return this.Custo * this.Quantidade;
        }
    }
}
