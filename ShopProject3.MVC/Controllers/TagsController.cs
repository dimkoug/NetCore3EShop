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
    [Authorize]
    public class TagsController : Controller
    {
        private readonly ITagsService _tagsService;

        public TagsController(ITagsService tagsService)
        {
            _tagsService = tagsService ?? throw new ArgumentNullException(nameof(tagsService));
        }

        public async Task<IActionResult> Index(int pageSize = 5, int totalItems = 5, int maxPages = 5, int pageNumber = 1)
        {
            var data = await _tagsService.GetAll();
            ViewBag.PageSizeList = new SelectList(new[] { 1, 5, 10, 20, 50, 100, 200, 500, 1000 }, pageSize);
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            data = data.Skip(ExcludeRecords).Take(pageSize).ToList();
            var counter = data.Count();

            var result = new PagedResult<Tags>
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
            return View("~/Views/Tags/Manage.cshtml", result);
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var model = await _tagsService.Get(Id);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tags viewModel)
        {

            if (ModelState.IsValid)
            {
                var model = await _tagsService.Add(viewModel);
                if (!String.IsNullOrEmpty(Request.Form["continue"]))
                {
                    return RedirectToAction("Edit", new { Id = model.Id });
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

            var viewModel = await _tagsService.Get(Id);

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, Tags viewModel)
        {
            if (Id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _tagsService.Update(viewModel);
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

            var viewModel = await _tagsService.Get(Id);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? Id, Tags viewModel)
        {
            await _tagsService.Delete(Id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> Exists(int id)
        {
            return await _tagsService.Exists(id);
        }
    }
}
