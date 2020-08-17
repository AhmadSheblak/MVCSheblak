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
    public class GradesController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Grades
        public ActionResult Index()
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            { //{ if (TempData["Role"] == db.User.SingleOrDefault(x=>x.RoleId==1)
              //    {
                GradeBLL u = new GradeBLL();
                var l = u.GetAll();
                return View(l);
                //}
            }
            return RedirectToAction("Error");
        }

        // GET: Grades/Details/5
        public ActionResult Details(int id)
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                string myString = id.ToString();
                if (myString == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                GradeBLL u = new GradeBLL();
                var isfound = u.FindById(id);
                if (!isfound)
                {
                    return HttpNotFound();
                }
                return View(u.grade);
            }
            return RedirectToAction("Error");
        }

        // GET: Grades/Create
        public ActionResult Create()
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                return View();
            }
            return RedirectToAction("Error");
        }

        // POST: Grades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Grade grade)
        {
            if (ModelState.IsValid)
            {
                GradeBLL u = new GradeBLL();
                var isucces = u.Add(grade);
                if (isucces)
                {
                    MessageBox.Show("The Grade Added");
                    return RedirectToAction("Index");
                }
            }

            return View(grade);
        }

        // GET: Grades/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                string myString = id.ToString();

                if (myString == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                GradeBLL u = new GradeBLL();
                var isfound = u.FindById(id);
                if (!isfound)
                {
                    return HttpNotFound();
                }
                Grade s = u.grade;
                return View(s);
            }
            return RedirectToAction("Error");
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GradeId,GradeName,Salary,Date")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                GradeBLL u = new GradeBLL();
                var isUpdated = u.Update(grade);
                if (isUpdated)
                {
                    MessageBox.Show("The Grade Updated");
                    return RedirectToAction("Index");
                }
            }
            return View(grade);
        }

        // GET: Grades/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                string myString = id.ToString();

                if (myString == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                GradeBLL u = new GradeBLL();
                var isfound = u.FindById(id);
                if (!isfound)
                {
                    return HttpNotFound();
                }
                return View(u.grade);
            }
            return RedirectToAction("Error");
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GradeBLL u = new GradeBLL();
            var isDeleted = u.Remove(id);
            if (isDeleted)
            {
                MessageBox.Show("The Grade Deleted");
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
