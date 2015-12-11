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

namespace ExpenseManager.Controllers
{   
    public class ExpensesController : Controller
    {
        IManagerRepository _ManagerRepository;

        public ExpensesController(IManagerRepository managerRepo)
        {
            _ManagerRepository = managerRepo;
        }
        
        public ActionResult Index()
        {
            return View(_ManagerRepository.GetExpenses(DateTime.Now.Year, DateTime.Now.Month));
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Amount,Description,ExpenseDate")] Expense expense)
        {
            expense.User = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                _ManagerRepository.AddExpense(expense);
                return RedirectToAction("Index");
            }

            return View(expense);
        }

        //// GET: Expenses/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Expense expense = db.Expenses.Find(id);
        //    if (expense == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(expense);
        //}





        //// GET: Expenses/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Expense expense = db.Expenses.Find(id);
        //    if (expense == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(expense);
        //}

        //// POST: Expenses/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Description,ExpenseDate,User")] Expense expense)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(expense).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(expense);
        //}

        //// GET: Expenses/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Expense expense = db.Expenses.Find(id);
        //    if (expense == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(expense);
        //}

        //// POST: Expenses/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Expense expense = db.Expenses.Find(id);
        //    db.Expenses.Remove(expense);
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
