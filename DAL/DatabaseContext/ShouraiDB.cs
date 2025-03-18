using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DatabaseContext
{
    public class ShouraiDB : DbContext
    {
        public ShouraiDB(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Entities.Account> Accounts { get; set; }
        public DbSet<Entities.Checkpoint> Checkpoints { get; set; }
        public DbSet<Entities.Equipment> Equipment { get; set; }
        public DbSet<Entities.InventoryItem> InventoryItems { get; set; }
        public DbSet<Entities.Payment> Payments { get; set; }
    }
}
