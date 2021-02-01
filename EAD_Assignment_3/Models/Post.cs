using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EAD_Assignment_3.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter title")]
        [StringLength(20)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter content")]
        [StringLength(500)]
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}
