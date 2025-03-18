using DAL.DatabaseContext;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories;

public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(ShouraiDB context) : base(context)
    {
    }

    public Payment GetByPaymentId(long paymentId)
    {
        var result = _query.FirstOrDefault(a => a.Id == paymentId);
        // Reset query for next operation
        return result;
    }
}
