using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Course
    {
        
        [Display(Name = "Grade ID")]
        public int CourseId { get; set; }
        [Required]
        [Display(Name = "Course Name")]
        public string Course_name { get; set; }
        [Display(Name = "Grade description")]
        public string Course_description { get; set; }

        public List<StudentInCourse> StudentsInCourse { get; set; }
    }
}
