using InventorySystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.DataAccess.Repository.IRepository
{
    public interface IWarehouseRepository:IRepository<Warehouse>
    {
        public void Update(Warehouse warehouse);
    }
}
