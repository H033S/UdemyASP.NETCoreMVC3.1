using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventorySystem.DataAccess.Repository.IRepository;
using InventorySystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WarehouseController : Controller
    {
        private readonly IWorkUnit _workUnit;

        public WarehouseController(IWorkUnit workUnit)
        {
            _workUnit = workUnit;
        }

        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Upsert(int? id)//Update or Insert
        {
            Warehouse warehouse = new Warehouse();
            if (id == null)
            {
                //This is to create a new Register
                return View(warehouse);
            }

            //For Update
            warehouse = _workUnit.Warehouse.Get(id.GetValueOrDefault());
            if (warehouse == null)
                return NotFound();

            return View(warehouse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                if (warehouse.Id == 0)
                {
                    _workUnit.Warehouse.Add(warehouse);
                }
                else
                {
                    _workUnit.Warehouse.Update(warehouse);
                }
                _workUnit.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }

        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _workUnit.Warehouse.GetAll();
            return Json(new { data = all });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var warehouseDb = _workUnit.Warehouse.Get(id);
                
            if(warehouseDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _workUnit.Warehouse.Remove(warehouseDb);
            _workUnit.Save();

            return Json(new
            {
                success = true,
                message = "Deleting Success"
            });
        }
        #endregion
    }
}
