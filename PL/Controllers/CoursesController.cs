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
    public class CoursesController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Courses
        public ActionResult Index()
        {

            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                CourseBLL u = new CourseBLL();
                var l = u.GetAll();
                return View(l);
            }
            return RedirectToAction("Error", "Home");
        }

        // GET: Courses/Details/5
        public ActionResult Details(int id)
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                string myString = id.ToString();
                if (myString == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CourseBLL u = new CourseBLL();
                var isfound = u.FindById(id);
                if (!isfound)
                {
                    return HttpNotFound();
                }
                return View(u.course);
            }
            return RedirectToAction("Error", "Home");
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                return View();
            }
            return RedirectToAction("Error", "Home");
            
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,Course_name,Course_description")] Course course)
        {
            if (ModelState.IsValid)
            {
                CourseBLL u = new CourseBLL();
                var isucces = u.Add(course);
                if (isucces)
                {
                    MessageBox.Show("The Course Added");
                    return RedirectToAction("Index");
                }
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int id)
        {
            
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                string myString = id.ToString();

                if (myString == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CourseBLL u = new CourseBLL();
                var isfound = u.FindById(id);
                if (!isfound)
                {
                    return HttpNotFound();
                }
                Course course = u.course;
                return View(course);
            }
            return RedirectToAction("Error", "Home");
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,Course_name,Course_description")] Course course)
        {
            if (ModelState.IsValid)
            {
                CourseBLL u = new CourseBLL();
                var isUpdated = u.Update(course);
                if (isUpdated)
                {
                    MessageBox.Show("The Course Updated");
                    return RedirectToAction("Index");
                }
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true)
            {
                string myString = id.ToString();

                if (myString == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CourseBLL u = new CourseBLL();
                var isfound = u.FindById(id);
                if (!isfound)
                {
                    return HttpNotFound();
                }
                return View(u.course);
            }
            return RedirectToAction("Error", "Home");
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseBLL u = new CourseBLL();
            var isDeleted = u.Remove(id);
            if (isDeleted)
            {
                MessageBox.Show("Course is Deleted");
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
