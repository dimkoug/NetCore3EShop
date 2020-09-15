using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Models;
using ShopProject3.MVC.Utilities;

namespace ShopProject3.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoriesService _categoriesService;
        private readonly IProductsService _productsService;

        public HomeController(ILogger<HomeController> logger, ICategoriesService categoriesService, IProductsService productsService)
        {
            _logger = logger;
            _categoriesService = categoriesService ?? throw new ArgumentNullException(nameof(categoriesService));
            _productsService = productsService ?? throw new ArgumentNullException(nameof(productsService));
        }

        public async Task<IActionResult> Index(string? q, int pageSize = 5, int totalItems = 5, int maxPages = 5, int pageNumber = 1)
        {
            var categories = (await _categoriesService.GetAll()).Where(c => c.Parent == null).ToList();
            var products = (await _productsService.GetAll()).ToList();
            if (!String.IsNullOrEmpty(q))
            {
                products = products.Where(c => c.Title.Contains(q, StringComparison.InvariantCultureIgnoreCase) || c.ProductCategories.Any(e => e.Category.Title.Contains(q, StringComparison.InvariantCultureIgnoreCase) || c.ProductTags.Any(f => f.Tag.Title.Contains(q, StringComparison.InvariantCultureIgnoreCase)) || c.Brand.Title.Contains(q, StringComparison.InvariantCultureIgnoreCase))).ToList();
            }
            ViewBag.TotalItemsList = new SelectList(new[] { 5, 10, 150, 500, 1000, 5000, 10000, 50000, 100000, 1000000 }, totalItems);
            ViewBag.PageSizeList = new SelectList(new[] { 1, 5, 10, 20, 50, 100, 200, 500, 1000 }, pageSize);
            ViewBag.MaxPagesList = new SelectList(new[] { 1, 5, 10, 20, 50, 100, 200, 500 }, maxPages);
            var counter = products.Count();
            ViewBag.Categories = categories.ToList();
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            products = products.Skip(ExcludeRecords).Take(pageSize).ToList();


            var result = new PagedResult<Products>
            {
                Data = products.ToList(),
                TotalItems = counter,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            ViewBag.Products = result;
            
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return View();
            }

            return View("~/Views/Home/Manage.cshtml");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
  
}