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
    public class StudentsController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Students
        public ActionResult Index()
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                StudentBLL u = new StudentBLL();
                var l = u.GetAll();
                return View(l);
            }
            return RedirectToAction("Error", "Home");
        }

        // GET: Students/Details/5
        public ActionResult Details(int id)
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                string myString = id.ToString();
                if (myString == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                StudentBLL u = new StudentBLL();
                var isfound = u.FindById(id);
                if (!isfound)
                {
                    return HttpNotFound();
                }
                return View(u.std);
            }
            return RedirectToAction("Error", "Home");
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                return View();
            }
            return RedirectToAction("Error", "Home");
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StdId,StdName,Grade_id,Std_address")] Student student)
        {
            if (ModelState.IsValid)
            {
                StudentBLL u = new StudentBLL();
                var isucces = u.Add(student);
                if (isucces)
                {
                    MessageBox.Show("The Student Added");
                    return RedirectToAction("Index");
                }
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                string myString = id.ToString();

                if (myString == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                StudentBLL u = new StudentBLL();
                var isfound = u.FindById(id);
                if (!isfound)
                {
                    return HttpNotFound();
                }
                Student s = u.std;
                return View(s);
            }
            return RedirectToAction("Error", "Home");
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StdId,StdName,Grade_id,Std_address")] Student student)
        {
            if (ModelState.IsValid)
            {
                StudentBLL u = new StudentBLL();
                var isUpdated = u.Update(student);
                if (isUpdated)
                {
                    MessageBox.Show("The Student Updated");
                    return RedirectToAction("Index");
                }
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                string myString = id.ToString();

                if (myString == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                StudentBLL u = new StudentBLL();
                var isfound = u.FindById(id);
                if (!isfound)
                {
                    return HttpNotFound();
                }
                return View(u.std);
            }
            return RedirectToAction("Error", "Home");
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentBLL u = new StudentBLL();
            var isDeleted = u.Remove(id);
            if (isDeleted)
            {
                MessageBox.Show("The Student Deleted");
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
