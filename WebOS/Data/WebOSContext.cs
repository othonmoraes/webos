using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebOS.Models
{
    public class WebOSContext : DbContext
    {
        public WebOSContext (DbContextOptions<WebOSContext> options)
            : base(options)
        {
        }

        public DbSet<WebOS.Models.Clientes> Clientes { get; set; }
        public DbSet<WebOS.Models.Tecnico_Motorista> Tecnico_Motorista { get; set; }

    }
}
