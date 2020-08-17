using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;

namespace BLL
{
    public class StudentInCourseBLL: IDisposable
    {
        MyContext _context;
        public StudentInCourse _studentincourse { get; private set; }
        public StudentInCourseBLL()
        {
            _context = new MyContext();

        }

        public bool FindById(int id)
        {
            _studentincourse = _context.StudentInCourse.Find();
            return _studentincourse != null;
        }

        public List<StudentInCourse> GetAll()
        {
            return _context.StudentInCourse.ToList();
        }

        public bool Add(StudentInCourse studentincourse)
        {
            try
            {
                _context.StudentInCourse.Add(studentincourse);
                return _context.SaveChanges() > 0;
            }
            catch
            {

                return false;
            }
        }

        public bool Remove(int Id)
        {
            if (FindById(Id))
            {
                _context.StudentInCourse.Remove(_studentincourse);
                return _context.SaveChanges() > 0;
            }
            else
                return false;
        }

        public bool Update(StudentInCourse studentincourse)
        {
            _context.Entry(studentincourse).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
