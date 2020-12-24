using InventorySystem.DataAccess.Data;
using InventorySystem.DataAccess.Repository.IRepository;
using InventorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace InventorySystem.DataAccess.Repository
{
    public class WarehouseRepository : Repository<Warehouse>, IWarehouseRepository
    {
        private readonly ApplicationDbContext _db;

        public WarehouseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Warehouse warehouse)
        {
            var warehouseDb = dbSet.FirstOrDefault(w => w.Id == warehouse.Id);

            if (warehouseDb == null)
                return;

            warehouseDb.Name = warehouse.Name;
            warehouseDb.Description = warehouse.Description;
            warehouseDb.State = warehouse.State;

        }
    }
}
