using ExpenseManager.Models;
using ExpenseManger.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseManager.Controllers
{
    public class ReportController : Controller
    {

        IManagerRepository _ManagerRepository;

        public ReportController(IManagerRepository managerRepo)
        {
            _ManagerRepository = managerRepo;
        }

        public ActionResult Index()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            foreach (var record in _ManagerRepository.GetCategoriesNames(User.Identity.GetUserId()))
            {
                categories.Add(new SelectListItem() { Text = record.Value, Value = record.Key.ToString() });
            }
            ViewBag.Category = categories;                                   

            ReportIndexViewModel m = new ReportIndexViewModel();
            m.Test = "this is my test";
            return View(m);
        }

        [HttpGet]
        public JsonResult GetCategoryReport(string category)
        {
            var result = _ManagerRepository
                            .GetReport(category,User.Identity.GetUserId());


            return Json(new { status = "Success", message = result }, JsonRequestBehavior.AllowGet);

        }
    }
}