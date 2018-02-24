using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpendingManagement.WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {   
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

    }
}