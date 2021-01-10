using Microsoft.AspNetCore.Mvc;
using MusicBox.DataAccess.Interfaces;
using MusicBox.Models.DbModels;

namespace MusicBox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
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
            var coverTypes = _unitOfWork.coverType.GetAll();
            return Json(new { data = coverTypes });
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
            CoverType coverType = new CoverType();
            if (id == null)
            {
                //This to create
                return View(coverType);
            }

            coverType = _unitOfWork.coverType.Get(id.Value);
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                if (coverType.Id == 0)
                {
                    _unitOfWork.coverType.Add(coverType);
                }
                else
                {
                    _unitOfWork.coverType.Update(coverType);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(coverType);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var deleteData = _unitOfWork.coverType.Get(id);
            if (deleteData == null)
            {
                return Json(new { success = false, message = "Data Not Found!" });
            }
            _unitOfWork.coverType.Remove(deleteData);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Operation Successfully!" });
        }
    }
}
