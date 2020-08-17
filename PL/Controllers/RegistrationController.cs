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
    public class RegistrationController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Registration
        //public ActionResult Index()
        //{
        //    var studentInCourse = db.StudentInCourse.Include(s => s.Courses).Include(s => s.Student);
        //    return View(studentInCourse.ToList());
        //}
        public ActionResult SelectCourse()
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true && (int)Session["UserType"] == 3)
            {
                ViewBag.CourseId = new SelectList(db.Course, "CourseId", "Course_name");
            return View();
            }
            return RedirectToAction("Error");
        }
        [HttpPost]
        public ActionResult SelectCourse(Course course)
        {
            Session["CourseSelected"] = course;
            return RedirectToAction("Details");
        }

        public ActionResult Details()
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true && (int)Session["UserType"] == 3)
            {
                var course= Session["CourseSelected"] as Course;
            if(course == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            string Test = course.CourseId.ToString();
            if (Test == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var info = db.StudentInCourse.SqlQuery("SELECT * FROM StudentInCourses WHERE CourseId=" + Test).ToList();
            if (info == null)
            {
                return HttpNotFound();
            }
            StudentBLL s = new StudentBLL();
            List<Student> ss = new List<Student>();
            foreach(var item in info)
            {
                if(s.FindById(item.studentID))
                {
                    ss.Add(s.std);
                }
            }
            return View(ss);
            }
            return RedirectToAction("Error", "Home");
        }

        // GET: Registration/Create
        public ActionResult Registration()
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true && (int)Session["UserType"] == 3)
            {
                ViewBag.CourseId = new SelectList(db.Course, "CourseId", "Course_name");
                ViewBag.studentID = new SelectList(db.Student, "StdId", "StdName");
                return View();
            }
            return RedirectToAction("Error", "Home");
        }

        // POST: Registration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Include = "Id,CourseId,studentID")] StudentInCourse studentInCourse)
        {
            if (ModelState.IsValid)
            {
                StudentInCourseBLL c = new StudentInCourseBLL();
                if(c.Add(studentInCourse))
                {
                    MessageBox.Show("The Registration Succeeded");
                    return RedirectToAction("SelectCourse");
                }
                
            }
                MessageBox.Show("The Registration Process Failed");
               
            ViewBag.CourseId = new SelectList(db.Course, "CourseId", "Course_name", studentInCourse.CourseId);
            ViewBag.studentID = new SelectList(db.Student, "StdId", "StdName", studentInCourse.studentID);
            return View(studentInCourse);
        }



        // GET: Registration/Delete/5
        public ActionResult UnRegistration(int id)
        {
            if (Session["isUser"] != null && (bool)Session["isUser"] == true && (int)Session["UserType"] == 3)
            {
                var course = Session["CourseSelected"] as Course;
                if (course == null)
                { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
                string test = id.ToString();
            if (test == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                var SIC = db.StudentInCourse.SingleOrDefault(x=>x.studentID== id && x.CourseId==course.CourseId);

                return View(SIC);
            }
            return RedirectToAction("Error", "Home");
        }

    

        // POST: Registration/Delete/5
        [HttpPost, ActionName("UnRegistration")]
        [ValidateAntiForgeryToken]
        public ActionResult UnRegistrationConfirmed(int id)
        {
            var course = Session["CourseSelected"] as Course;
            if (course == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            var SIC = db.StudentInCourse.SingleOrDefault(x => (x.CourseId == course.CourseId && x.studentID == id));
            db.StudentInCourse.Remove(SIC);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("The UnRegistration Succeeded");
                return RedirectToAction("SelectCourse");
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
