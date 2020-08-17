using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MyContext db = new MyContext();
            Course c = new Course()
            {
                CourseId = 1,
                Course_name = "OO Programming",
                Course_description = "Programming by Object Orinted Approuch"
            };
            db.Course.Add(c);
            db.SaveChanges();
            db.Dispose();
        }
    }
}
