using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class InventoryItem
    {
        [Key]
        public int Id { get; set; }
        public string ItemDataId { get; set; }
        public string Value { get; set; }

        public int AccountId { get; set; } //foreign key
        //navigation property
        public virtual Account Account { get; set; }
    }
}
