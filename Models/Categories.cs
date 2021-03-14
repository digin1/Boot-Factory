using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Boot_Factory.Models
{
    public class Categories
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        [StringLength(50, ErrorMessage = "Category should not exceed 50 characters")]
        public string Category { get; set; }
        public Categories()
        {

        }
    }
}
