using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class StudentInGradeBLL
    {
        MyContext _context ;
        public StudentInGrade _studentinGrade { get; private set; }
        public StudentInGradeBLL()
        {
            _context = new MyContext();

        }

        public bool FindById(int id)
        {
            _studentinGrade = _context.StudentInGrade.Find();
            return _studentinGrade != null;
        }

        public List<StudentInGrade> GetAll()
        {
            return _context.StudentInGrade.ToList();
        }

        public bool Add(StudentInGrade StudentInGrade1)
        {
            try
            {
                _context.StudentInGrade.Add(StudentInGrade1);
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
                _context.StudentInGrade.Remove(_studentinGrade);
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
