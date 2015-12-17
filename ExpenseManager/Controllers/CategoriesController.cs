using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExpenseManager.Models;
using ExpenseManger.Repository;
using Microsoft.AspNet.Identity;
using ExpenseManger.Model.HelperModels;

namespace ExpenseManager.Controllers
{
    public class CategoriesController : Controller
    {
        IManagerRepository _ManagerRepository;

        public CategoriesController(IManagerRepository managerRepo)
        {            
            _ManagerRepository = managerRepo;
        }
        
        public ActionResult Index()
        {
            var model = _ManagerRepository.GetCategories(User.Identity.GetUserId(),
                                      DateTime.Now.AddYears(-100), DateTime.Now.AddYears(100));
            return View(model == null ? new List<CategoryDetail>() : model);
        }
                       
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Plan,User")] Category category)
        {            
            category.User = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                OperationStatus opt = _ManagerRepository.AddCategory(category);
                if (!opt.Status)
                {
                    ModelState.AddModelError("", opt.Message);
                    return View(category);
                }                
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _ManagerRepository.GetCategory((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _ManagerRepository.GetCategory((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Plan")] Category category)
        {
            if (ModelState.IsValid)
            {
                OperationStatus opt = _ManagerRepository.UpdateCategory(category);
                if (!opt.Status)
                {
                    ModelState.AddModelError("", opt.Message);
                    return View(category);
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }
        
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _ManagerRepository.GetCategory((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            OperationStatus opt = _ManagerRepository.DeleteCategory(id);
            if (!opt.Status)
            {
                ModelState.AddModelError("", opt.Message);
                return View();
            }
            else
                return RedirectToAction("Index");
        }

    }
}
