using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Checkpoint
    {
        [Key]
        public int Id { get; set; }
        public string CheckpointId { get; set; }
        public bool Status { get; set; }
        public bool IsFinal { get; set; }
        public int AccountId { get; set; }
        //navigation property
        public virtual Account Account { get; set; }
    }
}
