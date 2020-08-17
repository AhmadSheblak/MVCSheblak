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
    public class RegisterInGradeController : Controller
    {
        private MyContext db = new MyContext();

        // GET: RegisterInGrade
        public ActionResult SelectGrade()
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true && (int)Session["UserType"]==3)
            {
                ViewBag.GradeId = new SelectList(db.Grade, "GradeId", "GradeName");
                return View();
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public ActionResult SelectGrade(Grade grade)
        {
            Session["GradeSelected"] = grade;
            return RedirectToAction("Details");
        }

        // GET: RegisterInGrade/Details/5
        public ActionResult Details()
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true && (int)Session["UserType"] == 3)
            {
                var grade = Session["GradeSelected"] as Grade;
                if (grade == null)
                { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
                string Test = grade.GradeId.ToString();
                if (Test == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var info = db.StudentInGrade.SqlQuery("SELECT * FROM StudentInGrades WHERE GradeId=" + Test).ToList();
                if (info == null)
                {
                    return HttpNotFound();
                }
                StudentBLL s = new StudentBLL();
                List<Student> ss = new List<Student>();
                foreach (var item in info)
                {
                    if (s.FindById(item.StudentId))
                    {
                        ss.Add(s.std);
                    }
                }
                return View(ss);
            }
            return RedirectToAction("Error", "Home");
        }

        // GET: RegisterInGrade/Create
        public ActionResult Registration()
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true && (int)Session["UserType"] == 3)
            {
                ViewBag.GradeID = new SelectList(db.Grade, "GradeId", "GradeName");
                ViewBag.StudentId = new SelectList(db.Student, "StdId", "StdName");
                return View();
            }
            return RedirectToAction("Error", "Home");
        }

        // POST: RegisterInGrade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Include = "id,StudentId,GradeID")] StudentInGrade studentInGrade)
        {
            if (ModelState.IsValid)
            {
                StudentInGradeBLL c = new StudentInGradeBLL();
                if (c.Add(studentInGrade))
                {
                    MessageBox.Show("The Registration Succeeded");
                    return RedirectToAction("SelectGrade");
                }

            }
            MessageBox.Show("The Registration Process Failed");

            ViewBag.GradeId = new SelectList(db.Course, "GradeId", "GradeName", studentInGrade.GradeID);
            ViewBag.studentID = new SelectList(db.Student, "StdId", "StdName", studentInGrade.StudentId);
            return View(studentInGrade);
        }

        // GET: RegisterInGrade/Delete/5
        public ActionResult UnRegistration(int id)
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true && (int)Session["UserType"] == 3)
            {
                var grade = Session["GradeSelected"] as Grade;
                if (grade == null)
                { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
                string test = id.ToString();
                if (test == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var SIG = db.StudentInGrade.SingleOrDefault(x => x.StudentId == id  &&  x.GradeID == grade.GradeId);
                return View(SIG);
            }
            return RedirectToAction("Error", "Home");
        }

        // POST: RegisterInGrade/Delete/5
        [HttpPost, ActionName("UnRegistration")]
        [ValidateAntiForgeryToken]
        public ActionResult UnRegistrationConfirmed(int id)
        {
            var grade = Session["GradeSelected"] as Grade;
            if (grade == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            var SIG = db.StudentInGrade.SingleOrDefault(x => x.StudentId == id && x.GradeID == grade.GradeId);
            db.StudentInGrade.Remove(SIG);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("The UnRegistration Succeeded");
                return RedirectToAction("SelectGrade");
            }
            MessageBox.Show("The Registration Process Failed");
            return UnRegistration(id);
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
