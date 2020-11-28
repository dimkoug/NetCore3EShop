using Microsoft.AspNetCore.Authorization;
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
    public class FeaturesController : Controller
    {
        private IFeatureService _productFeatureService;
        private IShopCategoryService _shopCategoryService;
        private IShopCategoryFeatureService _shopCategoryFeatureService;

        public FeaturesController(IFeatureService productFeatureService, IShopCategoryService shopCategoryService, IShopCategoryFeatureService shopCategoryFeatureService)
        {
            _productFeatureService = productFeatureService ?? throw new ArgumentNullException(nameof(productFeatureService));
            _shopCategoryService = shopCategoryService ?? throw new ArgumentNullException(nameof(shopCategoryService));
            _shopCategoryFeatureService = shopCategoryFeatureService ?? throw new ArgumentNullException(nameof(shopCategoryFeatureService));
        }


        public async Task<IActionResult> Index()
        {

            var controller = nameof(FeaturesController).Split("Controller")[0];
            var data = await _productFeatureService.GetAll();
            var html = "index";
            if (User.IsInRole(Roles.Owner) || User.Identity.IsAuthenticated)
            {
                html = "manage";
            }
            return View($"~/Views/{controller}/{html}.cshtml", data);

        }
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var controller = nameof(FeaturesController).Split("Controller")[0];
            var html = "detail";

            var model = await _productFeatureService.Get(Id);
            if (User.IsInRole(Roles.Owner) || User.Identity.IsAuthenticated)
            {
                html = "managedetail";
            }

            return View($"~/Views/{controller}/{html}.cshtml", model);
        }
        [Authorize]
        public async Task<IActionResult> Create(int? CategoryId)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            ViewBag.SelectedCategories = new SelectList(await _shopCategoryService.GetAll(), "Id", "Title", CategoryId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Feature viewModel, string[] ShopCategoryFeatures)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            if (ShopCategoryFeatures.Count() == 0)
            {
                ModelState.AddModelError("ShopCategoryFeatures", "Select at least one  category");
            }
            if (ModelState.IsValid)
            {
                await _shopCategoryFeatureService.Add(viewModel, ShopCategoryFeatures);
                var Id = await _productFeatureService.Add(viewModel);
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

            var viewModel = await _productFeatureService.Get(Id);
            viewModel.SelectedCategories = viewModel.ShopCategoryFeatures.Select(c => c.ShopCategoryId).AsEnumerable();
            ViewBag.SelectedCategories = new SelectList(await _shopCategoryService.GetAll(), "Id", "Title", viewModel.SelectedCategories);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int Id, Feature viewModel, string[] SelectedCategories)
        {
            if (Id == null)
            {
                return NotFound();
            }

            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            if (SelectedCategories.Count() == 0)
            {
                ModelState.AddModelError("SelectedCategories", "Select at least one  category");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _shopCategoryFeatureService.Delete(Id);
                    await _shopCategoryFeatureService.Add(viewModel, SelectedCategories);
                    await _productFeatureService.Update(viewModel);
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

            var viewModel = await _productFeatureService.Get(Id);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int? Id, Feature viewModel)
        {
            await _productFeatureService.Remove(Id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> Exists(int id)
        {
            return await _productFeatureService.Exists(id);
        }
    }
}
