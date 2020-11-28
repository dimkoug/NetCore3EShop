using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoreLinq;
using ShopProject.Domain.Interfaces;
using ShopProject.Domain.Models;
using ShopProject.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IShopCategoryService _shopCategoryService;
        private IShopCategoryFeatureService _shopCategoryFeatureService;
        private IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, IShopCategoryService shopCategoryService, IShopCategoryFeatureService shopCategoryFeatureService)
        {
            _logger = logger;
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _shopCategoryService = shopCategoryService ?? throw new ArgumentNullException(nameof(shopCategoryService));
            _shopCategoryFeatureService = shopCategoryFeatureService ?? throw new ArgumentNullException(nameof(shopCategoryFeatureService));
        }

        public async Task<IActionResult> Index(string? q, List<string> SelectedItems)
        {

            var categories = await _shopCategoryService.GetAll();
            var products = await _productService.GetAll();
            ViewBag.Features = (await _shopCategoryFeatureService.GetAll()).DistinctBy(c=>c.ProductFeature).ToList();
            if (!String.IsNullOrEmpty(q))
            {
                products = products.Where(c => c.Title.ToLower().Contains(q.ToLower()) || (!String.IsNullOrEmpty(c.Description) && c.Description.Contains(q)) || c.ProductShopCategories.Any(c=>c.ShopCategory.Title.ToLower().Contains(q)) || c.Brand.Title.Contains(q)).ToList();
            }
            if(SelectedItems != null && SelectedItems.Count()>0)
            {
                products = products.Where(c => c.ProductAttributes.Any(c=>SelectedItems.Contains(c.FeatureAttributes.Title))).ToList();
            }


            ViewBag.products = products;
            ViewBag.categories = categories.Where(c => c.Parent == null).ToList();
            ViewBag.q = q;
            ViewBag.SelectedItems = SelectedItems.ToList();
            return View();
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
