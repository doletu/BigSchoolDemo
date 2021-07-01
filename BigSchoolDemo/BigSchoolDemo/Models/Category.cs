using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BigSchoolDemo.Models
{
    public class Category
    {

        public int CategoryId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }


        public ICollection<Course> Courses { get; set; }
    }
}