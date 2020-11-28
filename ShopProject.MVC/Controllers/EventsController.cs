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
    public class EventsController : Controller
    {
        private IEventService _eventService;
        private IEventLocationService _eventLocationService;
        private IEventTagService _eventTagService;
        private IEventMediaService _eventMediaService;
        private ITagService _tagService;

        public EventsController(IEventService eventService, IEventLocationService eventLocationService, IEventTagService eventTagService, IEventMediaService eventMediaService, ITagService tagService)
        {
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
            _eventLocationService = eventLocationService ?? throw new ArgumentNullException(nameof(eventLocationService));
            _eventTagService = eventTagService ?? throw new ArgumentNullException(nameof(eventTagService));
            _eventMediaService = eventMediaService ?? throw new ArgumentNullException(nameof(eventMediaService));
            _tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));
        }



        public async Task<IActionResult> Index()
        {

            var controller = nameof(EventsController).Split("Controller")[0];
            var data = await _eventService.GetAll();
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
            var controller = nameof(EventsController).Split("Controller")[0];
            var html = "detail";

            var model = await _eventService.Get(Id);
            if (User.IsInRole(Roles.Owner) || User.Identity.IsAuthenticated)
            {
                html = "managedetail";
            }

            return View($"~/Views/{controller}/{html}.cshtml", model);
        }
        [Authorize]
        public async Task<IActionResult> Create(int? EventLocationId, int? TagId)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            var eventLocations = await _eventLocationService.GetAll();
            ViewBag.EventLocations = new SelectList(eventLocations.ToList(), "Id", "Title", EventLocationId);
            ViewBag.SelectedTags = new SelectList(await _tagService.GetAll(), "Id", "Title", TagId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Event viewModel, string[] EventTags, ICollection<IFormFile> files)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _eventMediaService.Add(viewModel, files);
                await _eventTagService.Add(viewModel, EventTags);
                var Id = await _eventService.Add(viewModel);
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
            var eventLocations = await _eventLocationService.GetAll();
            ViewBag.EventLocations = new SelectList(eventLocations.ToList(), "Id", "Title", viewModel.EventLocationId);

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

            var viewModel = await _eventService.Get(Id);
            var eventLocations = await _eventLocationService.GetAll();
            ViewBag.EventLocations = new SelectList(eventLocations.ToList(), "Id", "Title", viewModel.EventLocationId);
            viewModel.SelectedTags = viewModel.EventTags.Select(c => c.TagId).AsEnumerable();
            ViewBag.SelectedTags = new SelectList(await _tagService.GetAll(), "Id", "Title", viewModel.SelectedTags);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int Id,Event viewModel, string[] SelectedTags, ICollection<IFormFile> files)
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
                    await _eventTagService.Delete(Id);
                    await _eventTagService.Add(viewModel, SelectedTags);
                    await _eventMediaService.Add(viewModel, files);
                    await _eventService.Update(viewModel);
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

            var eventLocations = await _eventLocationService.GetAll();
            ViewBag.EventLocations = new SelectList(eventLocations.ToList(), "Id", "Title", viewModel.EventLocationId);
            viewModel.SelectedTags = viewModel.EventTags.Select(c => c.TagId).AsEnumerable();
            ViewBag.SelectedTags = new SelectList(await _tagService.GetAll(), "Id", "Title", viewModel.SelectedTags);
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

            var viewModel = await _eventService.Get(Id);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int? Id, Event viewModel)
        {
            await _eventService.Remove(Id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> Exists(int id)
        {
            return await _eventService.Exists(id);
        }
    }
}
