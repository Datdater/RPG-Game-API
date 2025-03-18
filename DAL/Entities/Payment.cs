using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities;

public class Payment
{
    public long Id { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; }
    public virtual Account Account { get; set; }
}
