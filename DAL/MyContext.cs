using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MyContext : DbContext
    {
        public MyContext() : base("name=MyContext")
        {

        }


        public DbSet<Course> Course { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<StudentInCourse> StudentInCourse { get; set; }
        public DbSet<StudentInGrade> StudentInGrade { get; set; }
    }
}
