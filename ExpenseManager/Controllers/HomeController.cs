using ExpenseManager.InfraStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseManager.Controllers
{
    public class HomeController :  Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        
        [AppAuthorize("FullAccessProfile")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}