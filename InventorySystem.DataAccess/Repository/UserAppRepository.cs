using InventorySystem.DataAccess.Data;
using InventorySystem.DataAccess.Repository.IRepository;
using InventorySystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.DataAccess.Repository
{
    public class UserAppRepository:Repository<UserApp>, IUserAppRepository
    {
        private readonly ApplicationDbContext _db;

        public UserAppRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
    }
}
