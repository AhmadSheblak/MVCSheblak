using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class StudentInGrade
    {
        public int id { get; set; }
        [Display(Name = "Student Name")]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Display(Name = "Grade Name")]
        [ForeignKey("Grade")]
        public int GradeID { get; set; }
        public Grade Grade { get; set; }


    }
}
