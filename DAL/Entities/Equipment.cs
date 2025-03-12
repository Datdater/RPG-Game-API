using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Equipment
    {
        public int Id { get; set; }
        public string EquipmentItemId { get; set; }
        public int AccountId { get; set; } //foreign key
        //navigation property
        public virtual Account Account { get; set; }
    }
}
