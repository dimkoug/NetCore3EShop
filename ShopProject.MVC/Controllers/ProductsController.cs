using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopProject.Domain.Interfaces;
using ShopProject.Domain.Models;
using ShopProject.MVC.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private IProductService _productService;
        private IProductMediaService _productMediaService;

        private IFeatureService _featureService;

        private IFeatureAttributeService _featureAttributeService;

        private IProductAttributeService _productAttributeService;
         
        private IProductTagService _productTagService;
        private IProductShopCategoryService _productShopCategoryService;

        private IShopCategoryService _shopCategoryService;
        private ITagService _tagService;
        private IBrandService _brandService;
        private IShopCategoryFeatureService _shopCategoryFeatureService;

        


        public ProductsController(IProductService productService, IShopCategoryService shopCategoryService, ITagService tagService,IBrandService brandService, IProductShopCategoryService productShopCategoryService, IProductTagService productTagService, IProductMediaService productMediaService, IProductAttributeService productAttributeService, IFeatureService featureService, IFeatureAttributeService featureAttributeService, IShopCategoryFeatureService shopCategoryFeatureService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _shopCategoryService = shopCategoryService ?? throw new ArgumentNullException(nameof(shopCategoryService));
            _tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));
            _brandService = brandService ?? throw new ArgumentNullException(nameof(brandService));
            _productShopCategoryService = productShopCategoryService ?? throw new ArgumentNullException(nameof(productShopCategoryService));
            _productTagService = productTagService ?? throw new ArgumentNullException(nameof(productTagService));
            _productMediaService = productMediaService ?? throw new ArgumentNullException(nameof(productMediaService));
            _productAttributeService = productAttributeService ?? throw new ArgumentNullException(nameof(productAttributeService));
            _featureService = featureService ?? throw new ArgumentNullException(nameof(featureService));
            _featureAttributeService = featureAttributeService ?? throw new ArgumentNullException(nameof(featureAttributeService));
            _shopCategoryFeatureService = shopCategoryFeatureService ?? throw new ArgumentNullException(nameof(shopCategoryFeatureService));
        }


        public async Task<IActionResult> Index()
        {

            var controller = nameof(ProductsController).Split("Controller")[0];
            var data = await _productService.GetAll();
            var html = "index";
            var categories = await _shopCategoryService.GetAll();
            ViewBag.categories = categories.Where(c => c.Parent == null).ToList();
            if (User.IsInRole(Roles.Owner) || User.Identity.IsAuthenticated)
            {
                html = "manage";
            }
            return View($"~/Views/{controller}/{html}.cshtml", data);

        }
        [Authorize]
        public async Task<IActionResult> AddProductAttribute(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            var model = await _productService.Get(productId);
            var vm = new ProductAttribute
            {
                ProductsId = model.Id,
            };
            ViewBag.Categories = new SelectList((await _shopCategoryService.GetAll()).ToList(), "Id", "Title");
            ViewBag.Features = new SelectList((await _featureService.GetAll()).ToList(), "Id", "Title");
            ViewBag.Attributes = new SelectList((await _featureAttributeService.GetAll()).ToList(), "Id", "Title");
            return View(vm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddProductAttribute(ProductAttribute viewModel)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var exists = await AttributeExists(viewModel.ProductsId, viewModel.FeatureAttributesId);
                if (!exists)
                {
                    viewModel.CreatedAt = DateTime.Now;
                    viewModel.UpdatedAt = DateTime.Now;
                    await _productAttributeService.Add(viewModel);
                }
                return RedirectToAction(nameof(Edit), new { @id = viewModel.ProductsId });
            }
            return View(viewModel);
        }
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var controller = nameof(ProductsController).Split("Controller")[0];
            var html = "detail";
            var categories = await _shopCategoryService.GetAll();
            ViewBag.categories = categories.Where(c => c.Parent == null).ToList();
            var model = await _productService.Get(Id);
            if (User.IsInRole(Roles.Owner) || User.Identity.IsAuthenticated)
            {
                html = "managedetail";
            }

            return View($"~/Views/{controller}/{html}.cshtml", model);
        }
        [Authorize]
        public async Task<IActionResult> Create(int? ParentId, int? CategoryId, int? TagId, int? BrandId)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            var products = await _productService.GetAll();
            ViewBag.Products = new SelectList(products.ToList(), "Id", "Title", ParentId);
            ViewBag.SelectedCategories = new SelectList(await _shopCategoryService.GetAll(), "Id", "Title", CategoryId);
            ViewBag.Brands = new SelectList(await _brandService.GetAll(), "Id", "Title", BrandId);
            ViewBag.SelectedTags = new SelectList(await _tagService.GetAll(), "Id", "Title", TagId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Product viewModel, string[] ProductShopCategories, string[] ProductTags, ICollection<IFormFile> files, IFormFile file)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            if (ProductShopCategories.Count() == 0)
            {
                ModelState.AddModelError("ProductShopCategories", "Select at least one  category");
            }
            if (ModelState.IsValid)
            {
                await _productService.AddFile(viewModel, file);
                await _productShopCategoryService.Add(viewModel, ProductShopCategories);
                await _productMediaService.Add(viewModel, files);
                await _productTagService.Add(viewModel, ProductTags);
                var Id = await _productService.Add(viewModel);
                if (!String.IsNullOrEmpty(Request.Form["continue"]))
                {
                    return RedirectToAction("Edit", new { Id = Id });
                }
                if (!String.IsNullOrEmpty(Request.Form["new"]))
                {
                    return RedirectToAction(nameof(Create));
                }
                return RedirectToAction(nameof(Index));
            }
            var products = await _productService.GetAll();
            ViewBag.Products = new SelectList(products.ToList(), "Id", "Title", viewModel.ParentId);
            ViewBag.Brands = new SelectList(await _brandService.GetAll(), "Id", "Title", viewModel.BrandId);
            viewModel.SelectedCategories = viewModel.ProductShopCategories.Select(c => c.ShopCategoryId).AsEnumerable();
            ViewBag.SelectedCategories = new SelectList(await _shopCategoryService.GetAll(), "Id", "Title", viewModel.SelectedCategories);
            viewModel.SelectedTags = viewModel.ProductTags.Select(c => c.TagId).AsEnumerable();
            ViewBag.SelectedTags = new SelectList(await _tagService.GetAll(), "Id", "Title", viewModel.SelectedTags);
            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            var viewModel = await _productService.Get(Id);
            var products = await _productService.GetAll();
            if(viewModel.ParentId != null)
            {
                ViewBag.Products = new SelectList(products.ToList(), "Id", "Title", viewModel.ParentId);
            }
            else
            {
                ViewBag.Products = new SelectList(products.ToList(), "Id", "Title");
            }
            
            ViewBag.Brands = new SelectList(await _brandService.GetAll(), "Id", "Title", viewModel.BrandId);
            viewModel.SelectedCategories = viewModel.ProductShopCategories.Select(c => c.ShopCategoryId).AsEnumerable();
            ViewBag.SelectedCategories = new SelectList(await _shopCategoryService.GetAll(), "Id", "Title", viewModel.SelectedCategories);
            viewModel.SelectedTags = viewModel.ProductTags.Select(c => c.TagId).AsEnumerable();
            ViewBag.SelectedTags = new SelectList(await _tagService.GetAll(), "Id", "Title", viewModel.SelectedTags);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int Id, Product viewModel, string[] SelectedCategories, string[] SelectedTags, ICollection<IFormFile> files, IFormFile file)
        {
            if (Id == null)
            {
                return NotFound();
            }

            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.AddFile(viewModel, file);
                    await _productShopCategoryService.Delete(Id);
                    await _productTagService.Delete(Id);
                    await _productMediaService.Add(viewModel, files);
                    await _productShopCategoryService.Add(viewModel, SelectedCategories);
                    await _productTagService.Add(viewModel, SelectedTags);
                    await _productService.Update(viewModel);
                    if (!String.IsNullOrEmpty(Request.Form["continue"]))
                    {
                        return RedirectToAction("Edit", new { Id = Id });
                    }
                    if (!String.IsNullOrEmpty(Request.Form["new"]))
                    {
                        return RedirectToAction(nameof(Create));
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (DBConcurrencyException)
                {
                    var exists = await Exists(viewModel.Id);
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            var products = await _productService.GetAll();
            ViewBag.Products = new SelectList(products.ToList(), "Id", "Title", viewModel.ParentId);
            ViewBag.Brands = new SelectList(await _brandService.GetAll(), "Id", "Title", viewModel.BrandId);
            viewModel.SelectedCategories = viewModel.ProductShopCategories.Select(c => c.ShopCategoryId).AsEnumerable();
            ViewBag.SelectedCategories = new SelectList(await _shopCategoryService.GetAll(), "Id", "Title", viewModel.SelectedCategories);
            viewModel.SelectedTags = viewModel.ProductTags.Select(c => c.TagId).AsEnumerable();
            ViewBag.SelectedTags = new SelectList(await _tagService.GetAll(), "Id", "Title", viewModel.SelectedTags);
            return View(viewModel);

        }

        [Authorize]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            var viewModel = await _productService.Get(Id);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int? Id, Product viewModel)
        {
            await _productService.Remove(Id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> Exists(int id)
        {
            return await _productService.Exists(id);
        }

        [Authorize]
        public async Task<IActionResult> DeleteAttribute(int ProductId, int FeatureAttributesId)
        {
            if (ProductId == null && FeatureAttributesId == null)
            {
                return NotFound();
            }
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            var viewModel = await _productAttributeService.Get(ProductId, FeatureAttributesId);

            return View(viewModel);
        }

        [HttpPost, ActionName("DeleteAttribute")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteAttributeConfirmed(int ProductId, int FeatureAttributesId, ProductAttribute viewModel)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            await _productAttributeService.Delete(ProductId, FeatureAttributesId);
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> getFeature(int? Id)
        {

            var features = (await _shopCategoryFeatureService.GetAll()).Where(c => c.ShopCategoryId == Id).Select(c => c.ProductFeature).ToList();


            return Json(features.ToList());
        }

        public async Task<IActionResult> getFeatureAttribute(int? Id)
        {
            var featureAttributes = (await _featureAttributeService.GetAll()).Where(c => c.FeatureId == Id).ToList();
            return Json(featureAttributes);
        }

        private async Task<bool> AttributeExists(int productId, int FeatureAttributeId)
        {
            return await _productAttributeService.Exists(productId, FeatureAttributeId);
        }


    
    }
}
