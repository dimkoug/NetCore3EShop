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
    public class BlogCategoriesController : Controller
    {
        private IBlogCategoryService _blogCategoryService;

        public BlogCategoriesController(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService ?? throw new ArgumentNullException(nameof(blogCategoryService));
        }



        public async Task<IActionResult> Index()
        {

            var controller = nameof(BlogCategoriesController).Split("Controller")[0];
            var data = await _blogCategoryService.GetAll();
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
            var controller = nameof(BlogCategoriesController).Split("Controller")[0];
            var html = "detail";

            var model = await _blogCategoryService.Get(Id);
            if (User.IsInRole(Roles.Owner) || User.Identity.IsAuthenticated)
            {
                html = "managedetail";
            }

            return View($"~/Views/{controller}/{html}.cshtml", model);
        }
        [Authorize]
        public async Task<IActionResult> Create(int? ParentId)
        {
            var categories = await _blogCategoryService.GetAll();
            ViewBag.Categories = new SelectList(categories.ToList(), "Id", "Title", ParentId);
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(BlogCategory viewModel, IFormFile file)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _blogCategoryService.AddFile(viewModel, file);
                var Id = await _blogCategoryService.Add(viewModel);
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
            var categories = await _blogCategoryService.GetAll();
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

            var viewModel = await _blogCategoryService.Get(Id);
            var categories = await _blogCategoryService.GetAll();
            ViewBag.Categories = new SelectList(categories.ToList(), "Id", "Title", viewModel.ParentId);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int Id, BlogCategory viewModel, IFormFile file)
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
                    await _blogCategoryService.AddFile(viewModel, file);
                    await _blogCategoryService.Update(viewModel);
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
            var categories = await _blogCategoryService.GetAll();
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

            var viewModel = await _blogCategoryService.Get(Id);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int? Id, BlogCategory viewModel)
        {
            await _blogCategoryService.Remove(Id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> Exists(int id)
        {
            return await _blogCategoryService.Exists(id);
        }
    }
}
