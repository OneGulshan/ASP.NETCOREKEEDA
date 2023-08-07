using ASPCORE.Data;
using ASPCORE.Helper;
using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace ASPCORE.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, string ?searchProductName = null)
        {
            var searchResult = _context.Product.AsNoTracking();
            if (!string.IsNullOrEmpty(searchProductName))
            {
                searchResult = _context.Product.Where(x => x.Title.Contains(searchProductName)).AsNoTracking();
                if (searchResult.Count() == 0)
                {
                    ViewBag.products = "Search Product Not Found";
                    return View();
                }
            }


            int pageSize = 3;//pageSize = Total No of records single page wise
            return View(await PaginatedList<Product>.CreateAsync(searchResult, pageNumber, pageSize));
        }
    }
}
