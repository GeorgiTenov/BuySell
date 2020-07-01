using Microsoft.EntityFrameworkCore;
using ProductLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodai.Data
{
    public class ProdaiContext : DbContext
    {
        public ProdaiContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        
    }
}
