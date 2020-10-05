using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAutentica.Models
{
    public class Total
    {

        public float TotalNotaFiscal { get; set; }
       
        public float getTotal()
        {
            return this.TotalNotaFiscal;
        }
    }
}
