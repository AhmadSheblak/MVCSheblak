using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StudentInCourse
    {
        public int Id { get; set; }
        [Display(Name = "Course Name")]
        [ForeignKey(nameof(Courses))]
        public int CourseId { get; set; }

        [Display(Name = "Student Name")]

        [ForeignKey(nameof(Student))]
        public int studentID { get; set; }
        public Course  Courses { get; set; }
        public Student Student { get; set; }
        //public int Mark { get; set; }

    }
}
