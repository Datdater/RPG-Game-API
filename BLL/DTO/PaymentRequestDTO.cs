using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO;

public class PaymentRequestDTO
{
    public string Username { get; set; }
    public decimal Money { get; set; }
}
