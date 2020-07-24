using IndusNetAssignMentRealTime1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndusNetAssignMentRealTime1.Controllers
{
    public class HomeController : Controller
    {
        private LogicContainer logic = new LogicContainer();
        [HttpGet]

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Register(string userFirstName, string userLastName, DateTime userDob, string userGender, string userEmail, string userPassword)
        {
            if (!String.IsNullOrEmpty(userFirstName) && !String.IsNullOrEmpty(userLastName) && userDob != null && !String.IsNullOrEmpty(userGender) && !String.IsNullOrEmpty(userEmail) && !String.IsNullOrEmpty(userPassword) && userPassword.Length>=6)
            {
                var result = logic.Register(userFirstName, userLastName, userDob, userGender, userEmail, userPassword);
                if (result == null)
                {
                    ViewBag.Err = "Internal server error. Can't create user";
                    return View();
                }
                else
                {
                    ViewBag.Msg = "Registration Successful";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                ViewBag.Err = "Fields can't be left empty and must match the criteria. Can't create user";
                return View();
            }
        }
        [HttpGet]

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(string userEmail, string userPassword)
        {
            if (!String.IsNullOrEmpty(userEmail) && !String.IsNullOrEmpty(userPassword))
            {
                var result = logic.Login(userEmail, userPassword);
                if (result == null)
                {
                    ViewBag.Err = "Cant't find the user";
                    return View();
                }
                else
                {
                    return View("Success", result);
                }
            }
            else
            {
                ViewBag.Err = "Fields can't be left empty. Can't login user";
                return View();
            }
        }
    }
}