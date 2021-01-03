using InventorySystem.DataAccess.Data;
using InventorySystem.DataAccess.Repository.IRepository;
using InventorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventorySystem.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Product product)
        {
            var productDb = dbSet.FirstOrDefault(p => p.Id == product.Id);

            if (productDb == null)
                return;

            if (product.ImageUrl != null)
                productDb.ImageUrl = product.ImageUrl;

            if (product.FatherId == 0)
                productDb.FatherId = null;
            else
                productDb.FatherId = product.FatherId;

            productDb.SerialNumber = product.SerialNumber;
            productDb.Description = product.Description;
            productDb.Price = product.Price;
            productDb.Cost = product.Cost;
            productDb.CategoryId = product.CategoryId;
            productDb.BrandId = product.BrandId;
        }
    }
}
