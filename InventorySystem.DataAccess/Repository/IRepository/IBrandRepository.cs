using InventorySystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.DataAccess.Repository.IRepository
{
    public interface IBrandRepository:IRepository<Brand>
    {
        public void Update(Brand brand);
    }
}
