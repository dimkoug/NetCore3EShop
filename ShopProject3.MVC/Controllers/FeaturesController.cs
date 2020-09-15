using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Models;

namespace ShopProject3.MVC.Controllers
{
    [Authorize(Roles ="Owner")]
    public class FeaturesController : Controller
    {
        private readonly IFeaturesService _featuresService;
        private readonly ICategoriesService _categoriesService;
        private readonly ICategoryFeaturesService _categoryFeaturesService;

        public FeaturesController(IFeaturesService featuresService, ICategoriesService categoriesService, ICategoryFeaturesService categoryFeaturesService)
        {
            _featuresService = featuresService ?? throw new ArgumentNullException(nameof(featuresService));
            _categoriesService = categoriesService ?? throw new ArgumentNullException(nameof(categoriesService));
            _categoryFeaturesService = categoryFeaturesService ?? throw new ArgumentNullException(nameof(categoryFeaturesService));
        }

        public async Task<IActionResult> Index()
        {
            var data = await _featuresService.GetAll();
            return View(data.ToList());
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var model = await _featuresService.Get(Id);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["SelectedCategories"] = new SelectList(await _categoriesService.GetAll(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Features viewModel, string[] CategoryFeatures)
        {
            if (CategoryFeatures.Count() == 0)
            {
                ModelState.AddModelError("CategoryFeatures", "Select at least one  category");
            }


            if (ModelState.IsValid)
            {
                await _categoryFeaturesService.Add(viewModel, CategoryFeatures);
                var Id = await _featuresService.Add(viewModel);
                if (!String.IsNullOrEmpty(Request.Form["continue"]))
                {
                    return RedirectToAction("Edit", new { Id = Id });
                }
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }


        public async Task<IActionResult> Edit(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var viewModel = await _featuresService.Get(Id);
            viewModel.SelectedCategories = viewModel.CategoryFeatures.Select(c => c.CategoryId).AsEnumerable();
            ViewData["SelectedCategories"] = new SelectList(await _categoriesService.GetAll(), "Id", "Title", viewModel.SelectedCategories);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, Features viewModel, string[] SelectedCategories)
        {
            if (Id == null)
            {
                return NotFound();
            }
            await _categoryFeaturesService.Delete(Id);

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryFeaturesService.Add(viewModel, SelectedCategories);
                    await _featuresService.Update(viewModel);
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


        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var viewModel = await _featuresService.Get(Id);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? Id, Features viewModel)
        {
            await _featuresService.Delete(Id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> Exists(int id)
        {
            return await _featuresService.Exists(id);
        }
    }
}
