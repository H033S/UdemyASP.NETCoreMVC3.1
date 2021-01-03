using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.DataAccess.Repository.IRepository
{
    public interface IWorkUnit:IDisposable
    {
        IBrandRepository Brand { get; }
        IWarehouseRepository Warehouse { get; }
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        IUserAppRepository User { get; }

        void Save();
    }
}
