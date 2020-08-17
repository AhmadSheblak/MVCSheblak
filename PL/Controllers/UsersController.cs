using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using BLL;
using System.Windows;

namespace PL.Controllers
{
    //Data Source=ReleaseSQLServer;Initial Catalog=MyReleaseDB;Integrated Security=True
    public class UsersController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Users
        public ActionResult Index()
        { 
            if (Session["isUser"] != null && (bool)Session["isUser"] == true && (int)Session["UserType"] == 1)
            {
                    UserBLL u = new UserBLL();
                    var l = u.GetAll();
                return View(l);
            }
            return RedirectToAction("Error");
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true && (int)Session["UserType"] == 1)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UserBLL u = new UserBLL();
                var isfound = u.FindById(id);
                if (!isfound)
                {
                    return HttpNotFound();
                }
                RoleBLL r = new RoleBLL();
                var find = r.FindById(u.user.RoleId);

                ViewBag.Role = r._Roles.RoleName;
                return View(u.user);
            }
            return RedirectToAction("Error");
        }
        public ActionResult DetailsforUser()
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                string username = Session["Username"] as String;
                UserBLL u = new UserBLL();
                var isfound = u.FindById(username);
                if (!isfound)
                {
                    return HttpNotFound();
                }
                RoleBLL r = new RoleBLL();
                var find=r.FindById(u.user.RoleId);

                ViewBag.Role = r._Roles.RoleName;
                return View(u.user);
            }
            
            return RedirectToAction("Error", "Home");
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                ViewBag.RoleId = new SelectList(db.Role, "RoleId", "RoleName");
            return View();
            }
            return RedirectToAction("Error");
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_name,Password,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                UserBLL u = new UserBLL();
                var isucces=u.Add(user);
                if (isucces)
                {
                    MessageBox.Show("The User Added");
                    return RedirectToAction("Index");
                }
            }

            ViewBag.RoleId = new SelectList(db.Role, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {

                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBLL u = new UserBLL();
            var isfound = u.FindById(id);
            if (!isfound)
            {
                return HttpNotFound();
            }
            User User = u.user;
            ViewBag.RoleId = new SelectList(db.Role, "RoleId", "RoleName", User.RoleId);
            return View(User);
            }
            return RedirectToAction("Error");
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_name,Password,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                UserBLL u = new UserBLL();
                var isUpdated = u.Update(user);
                if (isUpdated)
                {
                    MessageBox.Show("The User Updated");
                    return RedirectToAction("Index");
                }
            }
            ViewBag.RoleId = new SelectList(db.Role, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBLL u = new UserBLL();
            var isfound = u.FindById(id);
               
                
            if (!isfound)
            {
                return HttpNotFound();
            }
            return View(u.user);
            }
            return RedirectToAction("Error");
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserBLL u = new UserBLL();
            var isDeleted = u.Remove(id);
            if (isDeleted)
            {
                MessageBox.Show("User is Deleted");
                if (id == (String)Session["Username"])
                {
                    var check = db.User.SingleOrDefault(x => x.RoleId == 1);
                    if (check == null)
                    {
                        Session.Clear();
                        return RedirectToAction("Create", "FirstReg");
                    }
                    Session.Clear();
                    return RedirectToAction("Login", "Home");
                }
                return RedirectToAction("Index");
            }
            MessageBox.Show("The Deletion Process Failed");
            return View(User);
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
