﻿using InventorySystem.DataAccess.Data;
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

        public WorkUnit(ApplicationDbContext db)
        {
            _db = db;
            Warehouse = new WarehouseRepository(_db); // Initializing
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