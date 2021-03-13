using Boot_Factory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Boot_Factory.ViewModel
{
    public class MultipleData
    {
        public List<Products> Products { get; set; }
        public List<Categories> Categories { get; set; }


        public int Id { get; set; }
        [Required]
        [Display(Name = "Item name")]
        [StringLength(100, ErrorMessage = "Item name should not exceed 100 characters")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Image name")]
        [StringLength(100, ErrorMessage = "Image name should not exceed 100 characters")]
        public string ProductImage { get; set; }
        [Required]
        [Display(Name = "Item Description")]
        [StringLength(100, ErrorMessage = "Description should not exceed 200 characters")]
        public string ProductDescription { get; set; }
        [Required]
        [Display(Name = "Item Price")]
        public int ProductPrice { get; set; }
        [Required]
        [Display(Name = "Item Gender")]
        [StringLength(100, ErrorMessage = "Wrong input")]
        public string ProductGender { get; set; }
        [Required]
        [Display(Name = "Item Season")]
        [StringLength(100, ErrorMessage = "Wrong input")]
        public string ProductSeason { get; set; }

    }
}
