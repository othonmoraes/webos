using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace WebOS.Models
{
    public class Tecnico_Motorista
    {
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
    }
}
