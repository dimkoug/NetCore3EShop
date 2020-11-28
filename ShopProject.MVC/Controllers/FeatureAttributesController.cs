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
    public class FeatureAttributesController : Controller
    {
        private IFeatureAttributeService _featureAttributeService;
        private IFeatureService _featureService;

        public FeatureAttributesController(IFeatureAttributeService featureAttributeService, IFeatureService featureService)
        {
            _featureAttributeService = featureAttributeService ?? throw new ArgumentNullException(nameof(featureAttributeService));
            _featureService = featureService ?? throw new ArgumentNullException(nameof(featureService));
        }


        public async Task<IActionResult> Index()
        {

            var controller = nameof(FeatureAttributesController).Split("Controller")[0];
            var data = await _featureAttributeService.GetAll();
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
            var controller = nameof(FeatureAttributesController).Split("Controller")[0];
            var html = "detail";

            var model = await _featureAttributeService.Get(Id);
            if (User.IsInRole(Roles.Owner) || User.Identity.IsAuthenticated)
            {
                html = "managedetail";
            }

            return View($"~/Views/{controller}/{html}.cshtml", model);
        }
        [Authorize]
        public async Task<IActionResult> Create(int? FeatureId)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            var features = await _featureService.GetAll();
            ViewBag.Features = new SelectList(features.ToList(), "Id", "Title", FeatureId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(FeatureAttribute viewModel)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var Id = await _featureAttributeService.Add(viewModel);
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
            var features = await _featureService.GetAll();
            ViewBag.Features = new SelectList(features.ToList(), "Id", "Title", viewModel.FeatureId);

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

            var viewModel = await _featureAttributeService.Get(Id);
            var features = await _featureService.GetAll();
            ViewBag.Features = new SelectList(features.ToList(), "Id", "Title", viewModel.FeatureId);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int Id, FeatureAttribute viewModel)
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
                    await _featureAttributeService.Update(viewModel);
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

            var features = await _featureService.GetAll();
            ViewBag.Features = new SelectList(features.ToList(), "Id", "Title", viewModel.FeatureId);
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

            var viewModel = await _featureAttributeService.Get(Id);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int? Id, FeatureAttribute viewModel)
        {
            await _featureAttributeService.Remove(Id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> Exists(int id)
        {
            return await _featureAttributeService.Exists(id);
        }
    }
}
