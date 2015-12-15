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
        
        //public ActionResult Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category category = db.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}
        
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

        //// GET: Categories/Edit/5
        //public ActionResult Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category category = db.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}

        //// POST: Categories/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Name,Plan,User")] Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(category).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(category);
        //}

        //// GET: Categories/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category category = db.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}

        //// POST: Categories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    Category category = db.Categories.Find(id);
        //    db.Categories.Remove(category);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
