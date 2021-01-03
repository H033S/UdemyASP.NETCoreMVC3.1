using InventorySystem.DataAccess.Data;
using InventorySystem.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.DataAccess.Repository
{
    public class WorkUnit:IWorkUnit
    {
        private readonly ApplicationDbContext _db;
        public IWarehouseRepository Warehouse { get; }
        public ICategoryRepository Category { get; }

        public IBrandRepository Brand { get; }
        public IProductRepository Product { get; }

        public WorkUnit(ApplicationDbContext db)
        {
            _db = db;
            Warehouse = new WarehouseRepository(_db); // Initializing
            Category = new CategoryRepository(_db);
            Brand = new BrandRepository(_db);
            Product = new ProductRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
