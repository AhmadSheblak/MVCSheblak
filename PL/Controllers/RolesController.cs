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
    public class RolesController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Roles
        public ActionResult Index()
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            { //{ if (TempData["Role"] == db.User.SingleOrDefault(x=>x.RoleId==1)
              //    {
                RoleBLL u = new RoleBLL();
                var l = u.GetAll();
                return View(l);
                //}
            }
            return RedirectToAction("Error","Home");
        }

        // GET: Roles/Details/5
        public ActionResult Details(int id)
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                string myString = id.ToString();
                if (myString == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RoleBLL u = new RoleBLL();
                var isfound = u.FindById(id);
                if (!isfound)
                {
                    return HttpNotFound();
                }
                return View(u._Roles);
            }
            return RedirectToAction("Error");
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                return View();
            }
            return RedirectToAction("Error");
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleId,RoleName")] Role role)
        {
            if (ModelState.IsValid)
            {
                RoleBLL u = new RoleBLL();
                var isucces = u.Add(role);
                if (isucces)
                {
                    MessageBox.Show("The Role Added");
                    return RedirectToAction("Index");
                }
            }

            return View(role);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                string myString = id.ToString();

                if (myString == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RoleBLL u = new RoleBLL();
                var isfound = u.FindById(id);
                if (!isfound)
                {
                    return HttpNotFound();
                }
                Role s = u._Roles;
                return View(s);
            }
            return RedirectToAction("Error");
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoleId,RoleName")] Role role)
        {
            if (ModelState.IsValid)
            {
                RoleBLL u = new RoleBLL();
                var isUpdated = u.Update(role);
                if (isUpdated)
                {
                    MessageBox.Show("The Role Updated");
                    return RedirectToAction("Index");
                }
            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                string myString = id.ToString();

                if (myString == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RoleBLL u = new RoleBLL();
                var isfound = u.FindById(id);
                if (!isfound)
                {
                    return HttpNotFound();
                }
                return View(u._Roles);
            }
            return RedirectToAction("Error");
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoleBLL u = new RoleBLL();
            var isDeleted = u.Remove(id);
            if (isDeleted)
            {
                MessageBox.Show("The Role Deleted");
                return RedirectToAction("Index");
            }
            MessageBox.Show("The Deletion Process Failed");
            return RedirectToAction("Index");
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
