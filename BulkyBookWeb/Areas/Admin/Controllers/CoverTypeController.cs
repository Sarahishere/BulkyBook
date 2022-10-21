using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookModels;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;
[Area("Admin")]
public class CoverTypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CoverTypeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<CoverType> objectCoverTypeList = _unitOfWork.CoverType.GetAll();
        return View(objectCoverTypeList);
    }
    //get
    public IActionResult Create()
    {
        return View();
    }
   //post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType Created successfully";
            return RedirectToAction("Index");
        }

        return View();
    }
   //get
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var coverType = _unitOfWork.CoverType.GetFirstOrDefault(c=>c.Id==id);
        if (coverType == null)
        {
            return NotFound();
        }

        return View(coverType);
    }
    
    //post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType updated successfully";
            return RedirectToAction("Index");
        }

        return View(obj);
    }
    //get
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var obj =_unitOfWork.CoverType.GetFirstOrDefault(c=>c.Id == id);
        if (obj == null)
        {
            return NotFound();
        }

        return View(obj);
    } 
    //post
    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var obj = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
        if (obj == null)
        {
            return NotFound();
        }   
        _unitOfWork.CoverType.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "CoverType deleted successfully";
        return RedirectToAction("Index");

    }
}