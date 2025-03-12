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
    public class InventoryRepository : GenericRepository<InventoryItem>, IInventoryItemRepository
    {
        public InventoryRepository(ShouraiDB context) : base(context)
        {
        }
    }
}
