using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Grade
    {
        
        public int GradeId { get; set; }
        [Required]
        [Display(Name = "Grade Name")]
        public string GradeName { get; set; }
        [Required]
        public int Salary { get; set; }
        public string Date { get; set; }



        public List<StudentInGrade> StudentInGrade { get; set; }
    }
}
