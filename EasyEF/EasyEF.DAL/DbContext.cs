using System;
using System.Data.Entity;
using EasyEF.Models;

namespace EasyEF.DAL
{
    public class DbContext : System.Data.Entity.DbContext
    {
        public DbContext()
            : base("MyDbContext")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
