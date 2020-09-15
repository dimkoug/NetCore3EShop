using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Models;
using ShopProject3.MVC.Utilities;

namespace ShopProject3.MVC.Controllers
{
    public class OffersDetailController : Controller
    {
        private readonly IOffersDetailService _offersDetailService;
        private readonly IOffersService _offersService;
        private readonly IProductsService _productsService;
        public OffersDetailController(IOffersDetailService offersDetailService, IOffersService offersService, IProductsService productsService)
        {
            _offersDetailService = offersDetailService ?? throw new ArgumentNullException(nameof(offersDetailService));
            _offersService = offersService ?? throw new ArgumentNullException(nameof(offersService));
            _productsService = productsService ?? throw new ArgumentNullException(nameof(productsService));
        }
        [Authorize]
        public async Task<IActionResult> Index(int pageSize = 5, int totalItems = 5, int maxPages = 5, int pageNumber = 1)
        {
            var data = await _offersDetailService.GetAll();
            ViewBag.PageSizeList = new SelectList(new[] { 1, 5, 10, 20, 50, 100, 200, 500, 1000 }, pageSize);
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            data = data.Skip(ExcludeRecords).Take(pageSize).ToList();
            var counter = data.Count();

            var result = new PagedResult<OffersDetail>
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
            return View("~/Views/OffersDetail/Manage.cshtml", result);
        }
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var model = await _offersDetailService.Get(Id);
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return View(model);
            }


            return View("~/Views/OffersDetail/ManageDetail.cshtml", model);
        }
        [Authorize]
        public async Task<IActionResult> Create(int? offersId)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            ViewData["Offers"] = new SelectList(await _offersService.GetAll(), "Id", "Title", offersId);
            ViewData["Products"] = new SelectList(await _productsService.GetAll(), "Id", "Title");

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(OffersDetail viewModel)
        {
            if (ModelState.IsValid)
            {
                
                var Id = await _offersDetailService.Add(viewModel);
                if (!String.IsNullOrEmpty(Request.Form["continue"]))
                {
                    return RedirectToAction("Edit", new { Id = Id });
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Offers"] = new SelectList(await _offersService.GetAll(), "Id", "Title", viewModel.OffersId);
            ViewData["Products"] = new SelectList(await _productsService.GetAll(), "Id", "Title", viewModel.ProductsId);
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
            var viewModel = await _offersDetailService.Get(Id);
            ViewData["Offers"] = new SelectList(await _offersService.GetAll(), "Id", "Title", viewModel.OffersId);
            ViewData["Products"] = new SelectList(await _productsService.GetAll(), "Id", "Title", viewModel.ProductsId);
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int Id, OffersDetail viewModel)
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
                    var updatedId = await _offersDetailService.Update(viewModel);
                    if (!String.IsNullOrEmpty(Request.Form["continue"]))
                    {
                        return RedirectToAction("Edit", new { Id = updatedId });
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
            ViewData["Offers"] = new SelectList(await _offersService.GetAll(), "Id", "Title", viewModel.OffersId);
            ViewData["Products"] = new SelectList(await _productsService.GetAll(), "Id", "Title", viewModel.ProductsId);
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
            var viewModel = await _offersDetailService.Get(Id);
            return View(viewModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int? Id, OffersDetail viewModel)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            await _offersDetailService.Delete(Id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> Exists(int id)
        {
            return await _offersDetailService.Exists(id);
        }
    }
}
