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
    [Authorize(Roles = "Owner")]
    public class FeatureAttributesController : Controller
    {
        private readonly IFeatureAttributesService _featureAttributesService;
        private readonly IFeaturesService _featuresService;

        public FeatureAttributesController(IFeatureAttributesService featureAttributesService, IFeaturesService featuresService)
        {
            _featureAttributesService = featureAttributesService ?? throw new ArgumentNullException(nameof(featureAttributesService));
            _featuresService = featuresService ?? throw new ArgumentNullException(nameof(featuresService));
        }

        public async Task<IActionResult> Index()
        {
            var data = await _featureAttributesService.GetAll();
            return View(data.ToList());
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var model = await _featureAttributesService.Get(Id);
            return View(model);
        }

        public async Task<IActionResult> Create(int? featureId)
        {
            var features = await _featuresService.GetAll();
            ViewBag.Features = new SelectList(features.ToList(), "Id", "Title", featureId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeatureAttributes viewModel)
        {

            if (ModelState.IsValid)
            {
                var Id = await _featureAttributesService.Add(viewModel);
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

            var viewModel = await _featureAttributesService.Get(Id);
            var features = await _featuresService.GetAll();
            ViewBag.Features = new SelectList(features.ToList(), "Id", "Title", viewModel.FeatureId);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, FeatureAttributes viewModel)
        {
            if (Id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _featureAttributesService.Update(viewModel);
                    if (!String.IsNullOrEmpty(Request.Form["continue"]))
                    {
                        return RedirectToAction("Edit", new { Id = Id });
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (DBConcurrencyException)
                {
                    throw;
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

            var viewModel = await _featureAttributesService.Get(Id);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? Id, Categories viewModel)
        {
            await _featureAttributesService.Delete(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
