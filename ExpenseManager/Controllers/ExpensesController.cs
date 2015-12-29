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
    public class ExpensesController : Controller
    {
        IManagerRepository _ManagerRepository;

        public ExpensesController(IManagerRepository managerRepo)
        {
            _ManagerRepository = managerRepo;
        }
        
        public ActionResult Index()
        {
            var model = _ManagerRepository.GetExpenses(User.Identity.GetUserId(),
                                      DateTime.Now.AddYears(-100), DateTime.Now.AddYears(100));
            
            return View(model == null ? new List<Expense>() : model );
        }
        
        public ActionResult Create()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            foreach (var record in _ManagerRepository.GetCategoriesNames(User.Identity.GetUserId()))
            {
                categories.Add(new SelectListItem() { Text = record.Value, Value = record.Key.ToString()});
            }
            ViewBag.Category = categories;

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Amount,Description,ExpenseDate")] Expense expense, int category)        
        {            
            expense.User = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                OperationStatus opt = _ManagerRepository.AddExpense(expense, category);
                if (!opt.Status)
                {
                    ModelState.AddModelError("", opt.Message);
                    return View(expense);
                }
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = _ManagerRepository.GetExpense((int)id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = _ManagerRepository.GetExpense((int)id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Amount,Description,ExpenseDate")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                OperationStatus opt = _ManagerRepository.UpdateExpense(expense);
                if (!opt.Status)
                {
                    ModelState.AddModelError("", opt.Message);
                    return View(expense);
                }
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = _ManagerRepository.GetExpense((int)id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _ManagerRepository.DeleteExpense(id);            
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        [ActionName("_ExpenseList")]
        public PartialViewResult ExpenseList(int id)
        {
            var model = _ManagerRepository.GetExpenses(User.Identity.GetUserId(),
                                                       id,
                                                       DateTime.Now.AddYears(-100), 
                                                       DateTime.Now.AddYears(100));

            return PartialView(model == null ? new List<Expense>() : model);
        }

    }
}
