using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
     public class User
    {
        [Key]
        [Required]
        [Display(Name ="Username or ID")]
        public string User_name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [ForeignKey("UserRole")]
        public int RoleId { get; set; }
        public Role UserRole { get; set; }

    }
}
