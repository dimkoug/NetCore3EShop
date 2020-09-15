using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Models;
using ShopProject3.MVC.Utilities;

namespace ShopProject3.MVC.Controllers
{
    public class BannersController : Controller
    {
        private readonly IBannersService _bannersService;
        public BannersController(IBannersService bannersService)
        {
            _bannersService = bannersService ?? throw new ArgumentNullException(nameof(bannersService));
        }
        [Authorize]
        public async Task<IActionResult> Index(int pageSize = 5, int totalItems = 5, int maxPages = 5, int pageNumber = 1)
        {
            var data = await _bannersService.GetAll();
            ViewBag.PageSizeList = new SelectList(new[] { 1, 5, 10, 20, 50, 100, 200, 500, 1000 }, pageSize);
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            data = data.Skip(ExcludeRecords).Take(pageSize).ToList();
            var counter = data.Count();

            var result = new PagedResult<Banners>
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
            return View("~/Views/Banners/Manage.cshtml", result);
        }
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var model = await _bannersService.Get(Id);
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return View(model);
            }


            return View("~/Views/Banners/ManageDetail.cshtml", model);
        }
        [Authorize]
        public async Task<IActionResult> Create()
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Banners viewModel, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                await _bannersService.AddFile(viewModel, file);
                var Id = await _bannersService.Add(viewModel);
                if (!String.IsNullOrEmpty(Request.Form["continue"]))
                {
                    return RedirectToAction("Edit", new { Id = Id });
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
            var viewModel = await _bannersService.Get(Id);
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int Id, Banners viewModel)
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
                    var updatedId = await _bannersService.Update(viewModel);
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
            var viewModel = await _bannersService.Get(Id);
            return View(viewModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int? Id, Banners viewModel)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            await _bannersService.Delete(Id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> Exists(int id)
        {
            return await _bannersService.Exists(id);
        }
    }
}
