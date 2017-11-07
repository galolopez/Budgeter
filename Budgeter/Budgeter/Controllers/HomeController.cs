using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Budgeter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Account()
        {
            ViewBag.Title = "Account Page";

            return View();
        }

        public ActionResult Transaction()
        {
            ViewBag.Title = "Transaction Page";

            return View();
        }

        public ActionResult Household()
        {
            ViewBag.Title = "Household Page";

            return View();
        }

    }
}
