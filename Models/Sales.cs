using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Boot_Factory.Models
{
    public class Sales
    {

        // Validation not required : No Form for input
        public int Id { get; set; }
        public string CustomerEmail { get; set; }
        public int ProductId { get; set; }
        public Boolean SaleStatus { get; set; }

        public string ItemName { get; set; }

        public string ItemImage { get; set; }

        public int ItemCartPrice { get; set; }

        public Sales()
        {

        }
    }
}
