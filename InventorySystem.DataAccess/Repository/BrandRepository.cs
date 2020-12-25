using InventorySystem.DataAccess.Data;
using InventorySystem.DataAccess.Repository.IRepository;
using InventorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventorySystem.DataAccess.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private readonly ApplicationDbContext _db;

        public BrandRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Brand brand)
        {
            var brandDb = dbSet.FirstOrDefault(b => b.Id == brand.Id);

            if (brandDb == null)
                return;

            brandDb.Name = brand.Name;
            brandDb.State = brand.State;
        }
    }
}
