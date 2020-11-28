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
    public class ShopCategoriesController : Controller
    {
        private IShopCategoryService _shopCategoryService;
        private IShopCategoryFeatureService _shopCategoryFeatureService;

        public ShopCategoriesController(IShopCategoryService shopCategoryService, IShopCategoryFeatureService shopCategoryFeatureService)
        {
            _shopCategoryService = shopCategoryService ?? throw new ArgumentNullException(nameof(shopCategoryService));
            _shopCategoryFeatureService = shopCategoryFeatureService ?? throw new ArgumentNullException(nameof(shopCategoryFeatureService));
        }


        public async Task<IActionResult> Index()
        {

            var controller = nameof(ShopCategoriesController).Split("Controller")[0];
            var data = await _shopCategoryService.GetAll();
            var html = "index";
            ViewBag.Features = (await _shopCategoryFeatureService.GetAll());
            if (User.IsInRole(Roles.Owner) || User.Identity.IsAuthenticated)
            {
                html = "manage";
            }
            return View($"~/Views/{controller}/{html}.cshtml", data);

        }
        public async Task<IActionResult> Details(int? Id, List<string> SelectedItems)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var categories = await _shopCategoryService.GetAll();
            var controller = nameof(ShopCategoriesController).Split("Controller")[0];
            var html = "detail";

            var model = await _shopCategoryService.Get(Id);
            ViewBag.SelectedItems = SelectedItems;
            if (User.IsInRole(Roles.Owner) || User.Identity.IsAuthenticated)
            {
                html = "managedetail";
            }
            if (SelectedItems != null && SelectedItems.Count() > 0)
            {
                model.ProductShopCategories = model.ProductShopCategories.Where(c => c.Product.ProductAttributes.Any(c => SelectedItems.Contains(c.FeatureAttributes.Title))).ToList();
            }
            ViewBag.categories = categories.Where(c => c.Parent == null).ToList();

            return View($"~/Views/{controller}/{html}.cshtml", model);
        }
        [Authorize]
        public async Task<IActionResult> Create(int? ParentId)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            var categories = await _shopCategoryService.GetAll();
            ViewBag.Categories = new SelectList(categories.ToList(), "Id", "Title", ParentId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(ShopCategory viewModel, IFormFile file)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _shopCategoryService.AddFile(viewModel, file);
                var Id = await _shopCategoryService.Add(viewModel);
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
            var categories = await _shopCategoryService.GetAll();
            ViewBag.Categories = new SelectList(categories.ToList(), "Id", "Title", viewModel.ParentId);

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

            var viewModel = await _shopCategoryService.Get(Id);
            var categories = await _shopCategoryService.GetAll();
            ViewBag.Categories = new SelectList(categories.ToList(), "Id", "Title", viewModel.ParentId);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int Id, ShopCategory viewModel, IFormFile file)
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
                    await _shopCategoryService.AddFile(viewModel, file);
                    await _shopCategoryService.Update(viewModel);
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

            var categories = await _shopCategoryService.GetAll();
            ViewBag.Categories = new SelectList(categories.ToList(), "Id", "Title", viewModel.ParentId);
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

            var viewModel = await _shopCategoryService.Get(Id);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int? Id, ShopCategory viewModel)
        {
            await _shopCategoryService.Remove(Id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> Exists(int id)
        {
            return await _shopCategoryService.Exists(id);
        }
    }
}
