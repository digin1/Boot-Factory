
using Boot_Factory.Data;
using Boot_Factory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;



namespace Boot_Factory.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }


        //Home page product listing view
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

            return View(await _context.Products.ToListAsync());
        }


        // Get each product information
        [AllowAnonymous]
        public async Task<IActionResult> ItemPage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(products);
        }

        //Search for a product using keywords like Name,Season and Gender
        [HttpPost]
        [AllowAnonymous]
        public string Search(string searchText, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchText;
        }
        public async Task<IActionResult> Search(string searchText)
        {
            var item = from m in _context.Products
                         select m;

            if (!String.IsNullOrEmpty(searchText))
            {
                item = item.Where(s => s.ProductName.Contains(searchText) || s.ProductSeason.Equals(searchText) || s.ProductGender.Equals(searchText));
            }
            await item.ToListAsync();
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // List products for Men
        [AllowAnonymous]
        public async Task<IActionResult> Men()
        {
            var item = from m in _context.Products
                       select m;

            item = item.Where(s => s.ProductGender.Equals("Male") || s.ProductGender.Equals("Unisex"));

            return View(await item.ToListAsync());
        }

        // List products for Women
        [AllowAnonymous]
        public async Task<IActionResult> Women()
        {
            var item = from m in _context.Products
                       select m;

            item = item.Where(s => s.ProductGender.Equals("Female") || s.ProductGender.Equals("Unisex"));
            
            return View(await item.ToListAsync());
        }

        // List products for Hot Deals
        [AllowAnonymous]
        public async Task<IActionResult> HotDeals()
        {
            var item = from m in _context.Products
                       select m;

            item = item.Where(s => s.ProductSeason.Equals("Hot Deals"));

            return View(await item.ToListAsync());
        }

        // List products for New Arrivals
        [AllowAnonymous]
        public async Task<IActionResult> NewArrivals()
        {
            var item = from m in _context.Products
                       select m;

            item = item.Where(s => s.ProductSeason.Equals("New Arrivals"));

            return View(await item.ToListAsync());
        }

        // List products for Summer Sale
        [AllowAnonymous]
        public async Task<IActionResult> SummerSale()
        {
            var item = from m in _context.Products
                       select m;

            item = item.Where(s => s.ProductSeason.Equals("Summer Sale"));

            return View(await item.ToListAsync());
        }


        // Add to Cart
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddtoCart(int id)
        {
            //fetching product by id
            Products products = await _context.Products.FindAsync(id);

            //getting all fields of product
            var ifproduct = _context.Products.FirstOrDefault(b => b.Id == id);

            // getting current signed in user
            var userEmail = User.Identity.Name;


            if (userEmail != null)
            {
                if (ifproduct != null && User.IsInRole("Customer"))
                {
                    //Copying and inserting the product values and customer information to the table sales
                    _context.Sales.Add(new Sales { CustomerEmail = userEmail, ProductId = (int)id, SaleStatus = false, ItemName = ifproduct.ProductName, ItemImage = ifproduct.ProductImage, ItemCartPrice = ifproduct.ProductPrice });
                    // Updating session variable for basket count
                    var salesdata = from m in _context.Sales
                                    select m;

                    int salesdta = salesdata.Count(s => s.CustomerEmail.Equals(User.Identity.Name) && s.SaleStatus.Equals(false)) + 1;
                    HttpContext.Session.SetInt32("SessionCart", salesdta);
                }



                _context.SaveChanges();

            }
            return RedirectToAction(nameof(Index));
        }



        // Delete from cart 
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();


            // Updating session variable for basket count
            var salesdata = from m in _context.Sales
                            select m;

            int salesdta = salesdata.Count(s => s.CustomerEmail.Equals(User.Identity.Name) && s.SaleStatus.Equals(false));
            HttpContext.Session.SetInt32("SessionCart", salesdta);

            return RedirectToAction(nameof(Basket));
        }

        // Display About us page
        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        //Display Privacy Policy Page
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        // Checkout page : Updates Sales Table on checkout
        [Authorize(Roles = "Customer")]
        public IActionResult Checkout()
        {

            var sales = _context.Sales.Where(s => s.CustomerEmail.Equals(User.Identity.Name) && s.SaleStatus.Equals(false));

            foreach (var eachrow in sales)
            {
                eachrow.SaleStatus = true;
            }
            HttpContext.Session.SetInt32("SessionCart", 0);
            _context.SaveChanges();
            return View();
        }


         //Customer basket items with total count and total price
        [AllowAnonymous]
        public async Task<IActionResult> Basket()
        {
            var item = from pdt in _context.Products
                       join sal in _context.Sales on pdt.Id equals sal.ProductId
                       where sal.CustomerEmail == User.Identity.Name && sal.SaleStatus == false
                       select sal;
            ViewData["Sum"] = item.Sum(i => i.ItemCartPrice);

            int salesdta = item.Count(s => s.CustomerEmail.Equals(User.Identity.Name) && s.SaleStatus.Equals(false));
            ViewData["CartCount"] = salesdta;
            return View(await item.ToListAsync());
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // Customers purchased items list
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Orders ()
        {
            var item = from pdt in _context.Products
                       join sal in _context.Sales on pdt.Id equals sal.ProductId
                       where sal.CustomerEmail == User.Identity.Name && sal.SaleStatus == true
                       select sal;

            return View(await item.ToListAsync());

        }
    }
}
