using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using DAL;
using BLL;
namespace PL.Controllers
{
    public class FirstRegController : Controller
    {
        private MyContext db = new MyContext();

        // GET: FirstReg
        
        public ActionResult Create()
        {


            User check = db.User.SingleOrDefault(x => x.RoleId == 1);
            if (check==null)
            {
                return View();
            }
            return RedirectToAction("Error", "Home");
        }

        // POST: FirstReg/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_name,Password")] User user)
        {
            user.RoleId = 1;
            UserBLL u = new UserBLL();
            var isucces = u.Add(user);
            if (isucces)
            {
                MessageBox.Show("Done ♥! Please Login Now!");
                return RedirectToAction("Login","Home");
            }
            return View(user);
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
