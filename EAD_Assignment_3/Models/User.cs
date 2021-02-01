using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Imaging;
using System.ComponentModel.DataAnnotations;

namespace EAD_Assignment_3.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [StringLength(50)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter password again")]
        [StringLength(50)]
        public string Retype_Password { get; set; }
    }
    public class User1
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [StringLength(50)]
        public string Password { get; set; }

    }
}
