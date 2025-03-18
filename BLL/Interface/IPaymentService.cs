using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface;

public interface IPaymentService
{
    public Task<string> CreatePaymentLink(PaymentRequestDTO model);
    public Task ExcuteAddGold(string id);
}
