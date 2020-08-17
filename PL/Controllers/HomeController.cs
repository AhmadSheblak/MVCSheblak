using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using BLL;
using DAL;
namespace PL.Controllers
{
    public class HomeController : Controller
    {
        private MyContext db = new MyContext();
        public ActionResult Index()
        {
            var check = db.User.SingleOrDefault(x => x.RoleId == 1);
            if (check != null)
            {

                return View();
            }
            return RedirectToAction("Create", "FirstReg");
        }

        public ActionResult About()
        {
            ViewBag.Message = "School System by Ahmad Al Sheblak.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            
            UserBLL u = new UserBLL();
            if (u.FindById(user.User_name))
            {
                //TempData["Role"] = u.user.UserRole;
                if (user.Password == u.user.Password)
                {
                    Session["Username"] = u.user.User_name;
                    Session["isUser"] = true;
                    Session["UserType"] = u.user.RoleId;
                    Session["Username"] = u.user.User_name;
                    string Fullname = u.user.User_name;
                    MessageBox.Show("Welcome " + Fullname);
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("User_name", "Invalid Username or password!");
            return View(user);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        public ActionResult Contact()
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                ViewBag.Message = "Your contact page.";
                return View();
            }
            return RedirectToAction("Error");
        }
        public ActionResult Error()
        {

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}