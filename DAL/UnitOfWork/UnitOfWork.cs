using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DatabaseContext;
using DAL.IUnitOfWork.Interface;
using DAL.Repositories;
using DAL.Repositories.Interface;

public class UnitOfWork : IUnitOfWork
{
    private readonly ShouraiDB _context;
    public IAccountRepository AccountRepository { get; }

    public IInventoryItemRepository InventoryItemRepository { get; }

    public ICheckpointRepository CheckpointRepository { get; }

    public IEquipmentRepository EquipmentRepository { get; }

    public IPaymentRepository PaymentRepository { get; }

    public UnitOfWork(ShouraiDB context)
    {
        _context = context;
        AccountRepository = new AccountRepository(_context);
        InventoryItemRepository = new InventoryRepository(_context);
        CheckpointRepository = new CheckpointRepository(_context);
        EquipmentRepository = new EquipmentRepository(_context);
        PaymentRepository = new PaymentRepository(_context);

    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
		return await _context.SaveChangesAsync();
    }
}
