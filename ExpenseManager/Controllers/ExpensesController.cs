using ExpenseManager.Models;
using ExpenseManger.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace ExpenseManager.Controllers
{
    public class ExpensesController : Controller
    {
        IManagerRepository _ManagerRepository;

        public ExpensesController(IManagerRepository managerRepo)
        {
            _ManagerRepository = managerRepo;
        }
        
        public ActionResult Index(string startDate = "01/01/2000", string endDate = "01/01/2100")
        {
            var model = _ManagerRepository.GetExpenses(User.Identity.GetUserId(),
                                                Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));
            
            return View(model == null ? new List<Expense>() : model);
        }
        
        public ActionResult Create()
        {           
            ViewBag.CategoryID = GetCategories(User.Identity.GetUserId());
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Amount,Description,ExpenseDate,CategoryID")] Expense expense)        
        {            
            expense.User = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                OperationStatus opt = _ManagerRepository.AddExpense(expense);
                if (!opt.Status)
                {
                    ModelState.AddModelError("", opt.Message);
                    ViewBag.CategoryID = GetCategories(User.Identity.GetUserId());
                    return View(expense);
                }
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = GetCategories(User.Identity.GetUserId());
            return View(expense);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = _ManagerRepository.GetExpense((int)id, User.Identity.GetUserId());
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
            Expense expense = _ManagerRepository.GetExpense((int)id, User.Identity.GetUserId());
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Amount,Description,ExpenseDate,User")] Expense expense)
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
            Expense expense = _ManagerRepository.GetExpense((int)id, User.Identity.GetUserId());
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
            _ManagerRepository.DeleteExpense(id, User.Identity.GetUserId());            
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        [ActionName("_ExpenseList")]
        public PartialViewResult ExpenseList(int id)
        {
            IEnumerable<Expense> model = _ManagerRepository.GetExpenses(User.Identity.GetUserId(),
                                                                       id,
                                                                       DateTime.Now.AddYears(-100), 
                                                                       DateTime.Now.AddYears(100));

            return PartialView(model == null ? new List<Expense>() : model);
        }

        public List<SelectListItem> GetCategories (string userId)
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            foreach (var record in _ManagerRepository.GetCategoriesNames(userId))
            {
                categories.Add(new SelectListItem() { Text = record.Value, Value = record.Key.ToString() });
            }
            categories[0].Selected = true;
            return categories;
        }

    }
}
