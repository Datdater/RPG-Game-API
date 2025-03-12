using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Vitality { get; set; }
        public int Gold { get; set; }
        public string ScreenName { get; set; }

        //navigation property
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
        public virtual ICollection<Equipment> Equipment { get; set; }
        public virtual ICollection<Checkpoint> Checkpoints { get; set; }
    }
}
