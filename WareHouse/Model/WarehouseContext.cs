using System;
using System.ComponentModel;
using System.Data.Entity;

using WareHouse.Data;

namespace WareHouse.Model
{
    public class WarehouseContext : DbContext
    {
        public WarehouseContext(): base("Warehouse")
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Accept> Accepts { get; set; }
        public DbSet<InStorage> InStorages { get; set; }
        public DbSet<Sale> Sales { get; set; }

    }
}
