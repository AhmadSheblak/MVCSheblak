using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CourseBLL: IDisposable
    {
        MyContext Context;
        public Course course { get; private set; }
        public CourseBLL()
        {
            Context = new MyContext();

        }

        public bool FindById(int CourseId)
        {
            course = Context.Course.SingleOrDefault(x => x.CourseId == CourseId);
            return course != null;
        }

        public IEnumerable<Course> GetAll()
        {
            return Context.Course;
        }

        public bool Add(Course course)
        {
            try
            {
                Context.Course.Add(course);
                return Context.SaveChanges() > 0;
            }
            catch
            {

                return false;
            }
        }

        public bool Remove(int CourseID)
        {
            if (FindById(CourseID))
            {
                Context.Course.Remove(course);
                return Context.SaveChanges() > 0;
            }
            else
                return false;
        }

        public bool Update(Course course)
        {
            Context.Entry(course).State = EntityState.Modified;
            return Context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
