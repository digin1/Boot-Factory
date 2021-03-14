using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Boot_Factory.Models
{
    public class ContactUs
    {
        public int Id{ get; set; }
        [Required]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "Your name should not exceed 100 characters")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [StringLength(100, ErrorMessage = "Email should not exceed 100 characters")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Query")]
        [StringLength(200, ErrorMessage = "Query should not exceed 200 characters")]
        public string Query { get; set; }
        public ContactUs()
        {

        }
    }
}
