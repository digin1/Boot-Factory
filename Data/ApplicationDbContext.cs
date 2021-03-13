using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Boot_Factory.Models;

namespace Boot_Factory.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Boot_Factory.Models.Products> Products { get; set; }
        public DbSet<Boot_Factory.Models.ContactUs> ContactUs { get; set; }

        public DbSet<Boot_Factory.Models.Sales> Sales { get; set; }

        public DbSet<Boot_Factory.Models.Categories> Categories { get; set; }
    }
}
