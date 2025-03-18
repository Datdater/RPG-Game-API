using DAL.DatabaseContext;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(ShouraiDB context) : base(context)
        {
        }

        public Account GetByUsername(string username)
        {
            var result = _query.FirstOrDefault(a => a.Username == username);
            // Reset query for next operation
            return result;
        }
    }
}
