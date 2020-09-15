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
    public class EventsMediaController : Controller
    {
        private readonly IEventsMediaService _eventsMediaService;
        private readonly IEventsService _eventsService;
        public EventsMediaController(IEventsMediaService eventsMediaService, IEventsService eventsService)
        {
            _eventsMediaService = eventsMediaService ?? throw new ArgumentNullException(nameof(eventsMediaService));
            _eventsService = eventsService ?? throw new ArgumentNullException(nameof(eventsService));
        }
        [Authorize]
        public async Task<IActionResult> Index(int pageSize = 5, int totalItems = 5, int maxPages = 5, int pageNumber = 1)
        {
            var data = await _eventsMediaService.GetAll();
            ViewBag.PageSizeList = new SelectList(new[] { 1, 5, 10, 20, 50, 100, 200, 500, 1000 }, pageSize);
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            data = data.Skip(ExcludeRecords).Take(pageSize).ToList();
            var counter = data.Count();

            var result = new PagedResult<EventsMedia>
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
            return View("~/Views/EventsMedia/Manage.cshtml", result);
        }
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var model = await _eventsMediaService.Get(Id);
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return View(model);
            }


            return View("~/Views/EventsMedia/ManageDetail.cshtml", model);
        }
        [Authorize]
        public async Task<IActionResult> Create(int? eventId)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            ViewData["Events"] = new SelectList(await _eventsService.GetAll(), "Id", "Title", eventId);

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(EventsMedia viewModel, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                await _eventsMediaService.AddFile(viewModel, file);
                var Id = await _eventsMediaService.Add(viewModel);
                if (!String.IsNullOrEmpty(Request.Form["continue"]))
                {
                    return RedirectToAction("Edit", new { Id = Id });
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Events"] = new SelectList(await _eventsService.GetAll(), "Id", "Title", viewModel.EventsId);
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
            var viewModel = await _eventsMediaService.Get(Id);
            ViewData["Events"] = new SelectList(await _eventsService.GetAll(), "Id", "Title", viewModel.EventsId);
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int Id, EventsMedia viewModel)
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
                    var updatedId = await _eventsMediaService.Update(viewModel);
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
            ViewData["Events"] = new SelectList(await _eventsService.GetAll(), "Id", "Title", viewModel.EventsId);
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
            var viewModel = await _eventsMediaService.Get(Id);
            return View(viewModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int? Id, EventsMedia viewModel)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            await _eventsMediaService.Delete(Id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> Exists(int id)
        {
            return await _eventsMediaService.Exists(id);
        }
    }
}
