using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAutentica.Models
{
    public class NotaFiscal
    {
        public int NotaID { get; set; }
        public string Numero { get; set; }
        public float Valor { get; set; }
        public DateTime DataInsertSistema { get; set; }
       
    }
}
