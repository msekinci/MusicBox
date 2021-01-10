using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicBox.DataAccess.Interfaces;
using MusicBox.Models.DbModels;
using MusicBox.Models.ViewModels;
using System.Linq;

namespace MusicBox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        #region Actions
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region API CALLS
        public IActionResult GetAll()
        {
            var products = _unitOfWork.product.GetAll();
            return Json(new { data = products });
        }
        #endregion

        /// <summary>
        /// Create or Update Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductViewModel productViewModel = new ProductViewModel()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.CategoryName,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.coverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (id == null)
            {
                return View(productViewModel);
            }

            productViewModel.Product = _unitOfWork.product.Get(id.GetValueOrDefault());
            if (productViewModel.Product == null)
            {
                return NotFound();
            }
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Id == 0)
                {
                    _unitOfWork.product.Add(product);
                }
                else
                {
                    _unitOfWork.product.Update(product);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var deleteData = _unitOfWork.category.Get(id);
            if (deleteData == null)
            {
                return Json(new { success = false, message = "Data Not Found!" });
            }
            _unitOfWork.category.Remove(deleteData);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Operation Successfully!" });
        }
    }
}
