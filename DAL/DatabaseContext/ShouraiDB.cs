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
        DbSet<Entities.Account> Accounts { get; set; }
        DbSet<Entities.Checkpoint> Checkpoints { get; set; }
        DbSet<Entities.Equipment> Equipment { get; set; }
        DbSet<Entities.InventoryItem> InventoryItems { get; set; }
    }
}
