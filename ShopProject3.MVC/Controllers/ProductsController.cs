using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using JW;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Models;
using ShopProject3.MVC.Utilities;
using cloudscribe.Pagination.Models;

namespace ShopProject3.MVC.Controllers
{
  
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IProductFeatureAttributesService _productFeatureAttributesService;
        private readonly ICategoryFeaturesService _categoryFeaturesService;
        private readonly IFeaturesService _featuresService;
        private readonly IFeatureAttributesService _featureAttributesService;
        private readonly ICategoriesService _categoriesService;
        private readonly ITagsService _tagsService;
        private readonly IProductCategoriesService _productCategoriesService;
        private readonly IProductTagsService _productTagsService;
        private readonly IBrandsService _brandsService;
        private readonly IMediaService _documentsService;
        private readonly IProductMediaService _productMediaService;

        public Pager Pager { get; set; }
        public SelectList TotalItemsList { get; set; }
        public int TotalItems { get; set; }
        public SelectList PageSizeList { get; set; }
        public int PageSize { get; set; }
        public SelectList MaxPagesList { get; set; }
        public int MaxPages { get; set; }

        public ProductsController(IProductsService productsService, ICategoriesService categoriesService, ITagsService tagsService, IProductCategoriesService productCategoriesService, IProductTagsService productTagsService, IBrandsService brandsService, IMediaService documentsService, IProductMediaService productMediaService, IFeaturesService featuresService, IFeatureAttributesService featureAttributesService, ICategoryFeaturesService categoryFeaturesService, IProductFeatureAttributesService productFeatureAttributesService)
        {
            _productsService = productsService ?? throw new ArgumentNullException(nameof(productsService));
            _categoriesService = categoriesService ?? throw new ArgumentNullException(nameof(categoriesService));
            _tagsService = tagsService ?? throw new ArgumentNullException(nameof(tagsService));
            _productCategoriesService = productCategoriesService ?? throw new ArgumentNullException(nameof(productCategoriesService));
            _productTagsService = productTagsService ?? throw new ArgumentNullException(nameof(productTagsService));
            _brandsService = brandsService ?? throw new ArgumentNullException(nameof(brandsService));
            _documentsService = documentsService ?? throw new ArgumentNullException(nameof(documentsService));
            _productMediaService = productMediaService ?? throw new ArgumentNullException(nameof(productMediaService));
            _featuresService = featuresService ?? throw new ArgumentNullException(nameof(featuresService));
            _featureAttributesService = featureAttributesService ?? throw new ArgumentNullException(nameof(featureAttributesService));
            _categoryFeaturesService = categoryFeaturesService ?? throw new ArgumentNullException(nameof(categoryFeaturesService));
            _productFeatureAttributesService = productFeatureAttributesService ?? throw new ArgumentNullException(nameof(productFeatureAttributesService));
        }

        public async Task<IActionResult> Index(int pageSize=5, int totalItems=5, int maxPages=5, int pageNumber = 1)
        {
            var data = await _productsService.GetAll();

            ViewBag.TotalItemsList = new SelectList(new[] { 5, 10, 150, 500, 1000, 5000, 10000, 50000, 100000, 1000000 }, totalItems);
            ViewBag.PageSizeList = new SelectList(new[] { 1, 5, 10, 20, 50, 100, 200, 500, 1000 }, pageSize);
            ViewBag.MaxPagesList = new SelectList(new[] { 1, 5, 10, 20, 50, 100, 200, 500 }, maxPages);
            var counter = data.Count();
            //Pager = new Pager(data.Count(), pageNumber, PageSize, MaxPages);
            //data = data.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize).ToList();

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            data = data.Skip(ExcludeRecords).Take(pageSize).ToList();


            var result = new PagedResult<Products>
            {
                Data = data.ToList(),
                TotalItems = counter,
                PageNumber = pageNumber,
                PageSize = pageSize
            };



            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return View(result);
            }
            return View("~/Views/Products/Manage.cshtml", result);
        }




        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var model = await _productsService.Get(Id);
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return View(model);
            }
            return View("~/Views/Products/ManageDetail.cshtml", model);
        }
        [Authorize]
        public async Task<IActionResult> AddProductFeatureAttribute(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            var model = await _productsService.Get(productId);
            var vm = new ProductFeatureAttributes
            {
                ProductId = model.Id,
            };
            ViewData["Categories"] = new SelectList((await _categoriesService.GetAll()).ToList(), "Id", "Title");
            ViewData["Features"] = new SelectList((await _featuresService.GetAll()).ToList(), "Id", "Title");
            ViewData["Attributes"] = new SelectList((await _featureAttributesService.GetAll()).ToList(), "Id", "Title");
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddProductFeatureAttribute(ProductFeatureAttributes viewModel)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var exists = await AttributeExists(viewModel.ProductId, viewModel.FeatureAttributeId);
                if (!exists)
                {
                    viewModel.CreatedAt = DateTime.Now;
                    viewModel.UpdatedAt = DateTime.Now;
                    await _productFeatureAttributesService.Add(viewModel);
                }
                return RedirectToAction(nameof(Edit), new { @id = viewModel.ProductId });
            }
            return View(viewModel);
        }
        [Authorize]
        public async Task<IActionResult> Create(int? brandId, int? categoryId, int? tagId)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            ViewData["SelectedCategories"] = new SelectList(await _categoriesService.GetAll(), "Id", "Title", categoryId);
            ViewData["Products"] = new SelectList(await _productsService.GetAll(), "Id", "Title");
            ViewData["SelectedTags"] = new SelectList(await _tagsService.GetAll(), "Id", "Title", tagId);
            ViewData["Brands"] = new SelectList(await _brandsService.GetAll(), "Id", "Title", brandId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Products viewModel, string[] ProductCategories, string[] ProductTags)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            if (ProductCategories.Count() == 0)
            {
                ModelState.AddModelError("Categories", "Select at least one category");
            }

            if (ModelState.IsValid)
            {
                await _productCategoriesService.Add(viewModel, ProductCategories);
                await _productTagsService.Add(viewModel, ProductTags);
                var Id = await _productsService.Add(viewModel);
                if (!String.IsNullOrEmpty(Request.Form["continue"]))
                {
                    return RedirectToAction("Edit", new { Id = Id });
                }
                return RedirectToAction(nameof(Index));
            }
            viewModel.SelectedCategories = viewModel.ProductCategories.Select(c => c.CategoryId).AsEnumerable();
            viewModel.SelectedTags = viewModel.ProductTags.Select(c => c.TagId).AsEnumerable();
            ViewData["SelectedCategories"] = new SelectList(await _categoriesService.GetAll(), "Id", "Title", viewModel.SelectedCategories);
            ViewData["SelectedTags"] = new SelectList(await _tagsService.GetAll(), "Id", "Title", viewModel.SelectedTags);
            ViewData["Products"] = new SelectList(await _productsService.GetAll(), "Id", "Title", viewModel.ParentId);
            ViewData["Brands"] = new SelectList(await _brandsService.GetAll(), "Id", "Title", viewModel.BrandId);
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

            var viewModel = await _productsService.Get(Id);
            viewModel.SelectedCategories = viewModel.ProductCategories.Select(c => c.CategoryId).AsEnumerable();
            viewModel.SelectedTags = viewModel.ProductTags.Select(c => c.TagId).AsEnumerable();
            ViewData["SelectedCategories"] = new SelectList(await _categoriesService.GetAll(), "Id", "Title", viewModel.SelectedCategories);
            ViewData["SelectedTags"] = new SelectList(await _tagsService.GetAll(), "Id", "Title", viewModel.SelectedTags);
            ViewData["Products"] = new SelectList(await _productsService.GetAll(), "Id", "Title", viewModel.ParentId);
            ViewData["Brands"] = new SelectList(await _brandsService.GetAll(), "Id", "Title", viewModel.BrandId);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int Id, Products viewModel, string[] SelectedCategories, string[] SelectedTags, ICollection<IFormFile> Photos)
        {
            if (Id == null)
            {
                return NotFound();
            }
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            await _productCategoriesService.Delete(Id);
            await _productTagsService.Delete(Id);

            if (ModelState.IsValid)
            {
                try
                {
                    await _productCategoriesService.Add(viewModel, SelectedCategories);
                    await _documentsService.AddFiles(Photos);
                    await _productMediaService.AddFiles(viewModel, Photos);
                    await _productTagsService.Add(viewModel, SelectedTags);
                    await _productsService.Update(viewModel);
                    if (!String.IsNullOrEmpty(Request.Form["continue"]))
                    {
                        return RedirectToAction("Edit", new { Id = Id });
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

            var viewModel = await _productsService.Get(Id);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int? Id, Products viewModel)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            await _productsService.Delete(Id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> DeleteAttribute(int ProductId, int FeatureAttributeId)
        {
            if (ProductId == null && FeatureAttributeId == null)
            {
                return NotFound();
            }
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            var viewModel = await _productFeatureAttributesService.Get(ProductId, FeatureAttributeId);

            return View(viewModel);
        }

        [HttpPost, ActionName("DeleteAttribute")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteAttributeConfirmed(int ProductId, int FeatureAttributeId, ProductFeatureAttributes viewModel)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            await _productFeatureAttributesService.Delete(ProductId, FeatureAttributeId);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> Exists(int id)
        {
            return await _productsService.Exists(id);
        }

        public async Task<IActionResult> getFeature(int? Id)
        {

            var features = (await _categoryFeaturesService.GetAll()).Where(c => c.CategoryId == Id).Select(c => c.Feature).ToList();


            return Json(features.ToList());
        }

        public async Task<IActionResult> getFeatureAttribute(int? Id)
        {
            var featureAttributes = (await _featureAttributesService.GetAll()).Where(c => c.FeatureId == Id).ToList();
            return Json(featureAttributes);
        }

        private async Task<bool> AttributeExists(int productId, int FeatureAttributeId)
        {
            return await _productFeatureAttributesService.Exists(productId, FeatureAttributeId);
        }
    }
}

