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
            ReportIndexViewModel m = new ReportIndexViewModel();
            m.Test = "this is my test";
            return View(m);
        }

        [HttpGet]
        public JsonResult GetCategories(string searchVal)
        {
            var result = _ManagerRepository.GetCategoriesNames
                            (User.Identity.GetUserId()).Values.ToList();


            return Json(new { status = "Success", message = result }, JsonRequestBehavior.AllowGet);

        }
    }
}