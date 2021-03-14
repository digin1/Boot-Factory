using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Boot_Factory.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(100, ErrorMessage = "Firstname should not exceed 100 characters")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(100, ErrorMessage = "Lastname should not exceed 100 characters")]
        public string LastName { get; set; }

    }
}
