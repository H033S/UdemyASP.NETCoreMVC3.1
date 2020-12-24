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
    public class CategoryController : Controller
    {
        private readonly IWorkUnit _workUnit;

        public CategoryController(IWorkUnit workUnit)
        {
            _workUnit = workUnit;
        }

        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Upsert(int? id)//Update or Insert
        {
            Category category = new Category();
            if (id == null)
            {
                //This is to create a new Register
                return View(category);
            }

            //For Update
            category = _workUnit.Category.Get(id.GetValueOrDefault());
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    _workUnit.Category.Add(category);
                }
                else
                {
                    _workUnit.Category.Update(category);
                }
                _workUnit.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _workUnit.Category.GetAll();
            return Json(new { data = all });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var categoryDb = _workUnit.Category.Get(id);

            if (categoryDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _workUnit.Category.Remove(categoryDb);
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
