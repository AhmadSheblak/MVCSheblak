using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;

namespace BLL
{
    public class StudentBLL:IDisposable
    {
        MyContext _context;
        public Student std { get; private set; }
        public StudentBLL()
        {
            _context = new MyContext();

        }

        public bool FindById(int id)
        {
            std = _context.Student.SingleOrDefault(x => x.StdId == id);
            return std != null;
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Student;
        }

        public bool Add(Student student)
        {
            try
            {
                _context.Student.Add(student);
                return _context.SaveChanges() > 0;
            }
            catch
            {

                return false;
            }
        }

        public bool Remove(int id)
        {
            if (FindById(id))
            {
                _context.Student.Remove(std);
                return _context.SaveChanges() > 0;
            }
            else
                return false;
        }

        public bool Update(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }




        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
