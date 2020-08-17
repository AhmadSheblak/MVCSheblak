using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Student
    {
        [Key]
        public int StdId { get; set; }
        [Required]
        [Display(Name = "Student Name")]
        public string StdName { get; set; }
        public int Grade_id { get; set; }
        [Required]
        [Display(Name = "Student Address")]
        public string Std_address { get; set; }
       

        public List<StudentInCourse>  StudentInCourse { get; set; }
        public List<StudentInGrade> StudentInGrade { get; set; }



    }
}
