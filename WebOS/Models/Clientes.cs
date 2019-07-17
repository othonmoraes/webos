using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models
{
    public class Clientes
    {
        [Key]
        public int Codigo { get; set; }
        public String NomeRazaoSocial { get; set; }
        public string Telefone_fixo { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }

    }
}
