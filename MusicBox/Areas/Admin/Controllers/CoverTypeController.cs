using Dapper;
using Microsoft.AspNetCore.Mvc;
using MusicBox.DataAccess.Interfaces;
using MusicBox.Models.DbModels;
using MusicBox.Utility;

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
            //Entity
            //var coverTypes = _unitOfWork.coverType.GetAll();

            //Dapper
            var coverTypes = _unitOfWork.spCall.List<CoverType>(ProjectConstant.Proc_CoverType_GetAll, null);
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

            //Entity
            //coverType = _unitOfWork.coverType.Get(id.Value);

            //Dapper
            var parameter = new DynamicParameters();
            
            coverType = _unitOfWork.spCall.OneRecord<CoverType>(ProjectConstant.Proc_CoverType_Get, parameter);
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
                var parameter = new DynamicParameters();
                
                if (coverType.Id == 0)
                {
                    //_unitOfWork.coverType.Add(coverType);
                    parameter.Add("@Name", coverType.Name);
                    _unitOfWork.spCall.Execute(ProjectConstant.Proc_CoverType_Create, parameter);
                }
                else
                {
                    //_unitOfWork.coverType.Update(coverType);
                    parameter.Add("@Id", coverType.Id);
                    _unitOfWork.spCall.Execute(ProjectConstant.Proc_CoverType_Update, parameter);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(coverType);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //Entity
            //var deleteData = _unitOfWork.coverType.Get(id);
            //if (deleteData == null)
            //{
            //    return Json(new { success = false, message = "Data Not Found!" });
            //}
            //_unitOfWork.coverType.Remove(deleteData);

            //Dapper
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var deleteData = _unitOfWork.spCall.OneRecord<CoverType>(ProjectConstant.Proc_CoverType_Get, parameter);
            if (deleteData == null)
            {
                return Json(new { success = false, message = "Data Not Found!" });
            }
            _unitOfWork.spCall.Execute(ProjectConstant.Proc_CoverType_Delete, parameter);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Operation Successfully!" });
        }
    }
}
