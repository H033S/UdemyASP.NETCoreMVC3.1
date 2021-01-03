using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InventorySystem.DataAccess.Repository.IRepository;
using InventorySystem.Models;
using InventorySystem.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventorySystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IWorkUnit _workUnit;
        private readonly IWebHostEnvironment _hostEnviroment;

        public ProductController(IWorkUnit workUnit, IWebHostEnvironment hostEnvironment)
        {
            _workUnit = workUnit;
            _hostEnviroment = hostEnvironment;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Upsert(int? id)//Update or Insert
        {
            ProductViewModel productViewModel = new ProductViewModel()
            {
                Product = new Product(),
                CategoryList = _workUnit.Category.GetAll().Select(c => new SelectListItem {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                }),
                BrandList = _workUnit.Brand.GetAll().Select(b => new SelectListItem {
                    Text = b.Name,
                    Value = b.Id.ToString(),
                }),
                FatherList = _workUnit.Product.GetAll().Select(p => new SelectListItem
                {
                    Text = p.Description,
                    Value = p.Id.ToString(),
                })
            };
            if (id == null)
            {
                //This is to create a new Register
                return View(productViewModel);
            }

            //For Update
            productViewModel.Product = _workUnit.Product.Get(id.GetValueOrDefault());
            if (productViewModel.Product == null)
                return NotFound();

            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                //Load Images
                string webRootPath = _hostEnviroment.WebRootPath;
                var files = HttpContext.Request.Form.Files;//Get files from form

                if (files.Count > 0)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\products");
                    var extension = Path.GetExtension(files[0].FileName);

                    if (productViewModel.Product.ImageUrl != null)
                    {
                        //To edit, we need remove the olderone
                        var imagePath = Path.Combine(webRootPath, productViewModel.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    using (var filesStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    productViewModel.Product.ImageUrl = @"\images\products\" + filename + extension;
                }
                else
                {
                    //Update and the user doesnt change the image 
                    if (productViewModel.Product.Id != 0)
                    {
                        Product productDb = _workUnit.Product.Get(productViewModel.Product.Id);
                        productViewModel.Product.ImageUrl = productDb.ImageUrl;
                    }
                }


                if (productViewModel.Product.Id == 0)
                {
                    _workUnit.Product.Add(productViewModel.Product);
                }
                else
                {
                    _workUnit.Product.Update(productViewModel.Product);
                }

                _workUnit.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                productViewModel.CategoryList = _workUnit.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                });
                productViewModel.BrandList = _workUnit.Brand.GetAll().Select(b => new SelectListItem
                {
                    Text = b.Name,
                    Value = b.Id.ToString(),
                });
                productViewModel.FatherList = _workUnit.Product.GetAll().Select(p => new SelectListItem
                {
                    Text = p.Description,
                    Value = p.Id.ToString(),
                });

                if (productViewModel.Product.Id != 0)
                    productViewModel.Product = _workUnit.Product.Get(productViewModel.Product.Id);
            }

            return View(productViewModel.Product);
        }

        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _workUnit.Product.GetAll(propertyIncluding: "Category,Brand");
            return Json(new { data = all });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var productDb = _workUnit.Product.Get(id);

            if (productDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            //To delete the product object we need remove too the product image
            string webRootPath = _hostEnviroment.WebRootPath;

            string imagePath = Path.Combine(webRootPath, productDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _workUnit.Product.Remove(productDb);
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
