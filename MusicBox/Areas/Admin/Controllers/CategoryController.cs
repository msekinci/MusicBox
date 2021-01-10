using Microsoft.AspNetCore.Mvc;
using MusicBox.DataAccess.Interfaces;
using MusicBox.Models.DbModels;

namespace MusicBox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            var categories = _unitOfWork.category.GetAll();
            return Json(new { data = categories });
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
            Category category = new Category();
            if (id == null)
            {
                //This to create
                return View(category);
            }

            category = _unitOfWork.category.Get(id.Value);
            if (category == null)
            {
                return NotFound();
            }
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
                    _unitOfWork.category.Add(category);
                }
                else
                {
                    _unitOfWork.category.Update(category);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(category);
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
