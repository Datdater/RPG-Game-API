using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IUnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        IInventoryItemRepository InventoryItemRepository { get; }
        ICheckpointRepository CheckpointRepository { get; }
        IEquipmentRepository EquipmentRepository { get; }
        IPaymentRepository PaymentRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
