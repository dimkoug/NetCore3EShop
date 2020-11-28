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
    public class BlogPostsController : Controller
    {
        private IBlogPostService _blogPostService;
        private IBlogCategoryService _blogCategoryService;
        private ITagService _tagService;
        private IBlogPostCategoryService _blogPostCategoryService;
        private IBlogPostTagService _blogPostTagService;
        private IBlogPostMediaService _blogPostMediaService;

        public BlogPostsController(IBlogPostService blogPostService, IBlogCategoryService blogCategoryService, ITagService tagService, IBlogPostCategoryService blogPostCategoryService, IBlogPostTagService blogPostTagService, IBlogPostMediaService blogPostMediaService)
        {
            _blogPostService = blogPostService ?? throw new ArgumentNullException(nameof(blogPostService));
            _blogCategoryService = blogCategoryService ?? throw new ArgumentNullException(nameof(blogCategoryService));
            _tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));
            _blogPostCategoryService = blogPostCategoryService ?? throw new ArgumentNullException(nameof(blogPostCategoryService));
            _blogPostTagService = blogPostTagService ?? throw new ArgumentNullException(nameof(blogPostTagService));
            _blogPostMediaService = blogPostMediaService ?? throw new ArgumentNullException(nameof(blogPostMediaService));
        }



        public async Task<IActionResult> Index()
        {

            var controller = nameof(BlogPostsController).Split("Controller")[0];
            var data = await _blogPostService.GetAll();
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
            var controller = nameof(BlogPostsController).Split("Controller")[0];
            var html = "detail";

            var model = await _blogPostService.Get(Id);
            if (User.IsInRole(Roles.Owner) || User.Identity.IsAuthenticated)
            {
                html = "managedetail";
            }

            return View($"~/Views/{controller}/{html}.cshtml", model);
        }
        [Authorize]
        public async Task<IActionResult> Create(int? CategoryId, int? TagId)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            ViewBag.SelectedCategories = new SelectList(await _blogCategoryService.GetAll(), "Id", "Title", CategoryId);
            ViewBag.SelectedTags = new SelectList(await _tagService.GetAll(), "Id", "Title", TagId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(BlogPost viewModel, string[] BlogPostCategories, string[] BlogPostsTags, ICollection<IFormFile> files)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            if (BlogPostCategories.Count() == 0)
            {
                ModelState.AddModelError("BlogPostCategories", "Select at least one  category");
            }
            if (ModelState.IsValid)
            {
                
                await _blogPostCategoryService.Add(viewModel, BlogPostCategories);
                await _blogPostMediaService.Add(viewModel, files);
                await _blogPostTagService.Add(viewModel, BlogPostsTags);
                var Id = await _blogPostService.Add(viewModel);
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

            var viewModel = await _blogPostService.Get(Id);
            viewModel.SelectedCategories = viewModel.BlogPostCategories.Select(c => c.BlogCategoryId).AsEnumerable();
            ViewBag.SelectedCategories = new SelectList(await _blogCategoryService.GetAll(), "Id", "Title", viewModel.SelectedCategories);
            viewModel.SelectedTags = viewModel.BlogPostTags.Select(c => c.TagId).AsEnumerable();
            ViewBag.SelectedTags = new SelectList(await _tagService.GetAll(), "Id", "Title", viewModel.SelectedTags);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int Id, BlogPost viewModel, string[] SelectedCategories, string[] SelectedTags, ICollection<IFormFile> files)
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
                    await _blogPostCategoryService.Delete(Id);
                    await _blogPostTagService.Delete(Id);
                    await _blogPostMediaService.Add(viewModel, files);
                    await _blogPostCategoryService.Add(viewModel, SelectedCategories);
                    await _blogPostTagService.Add(viewModel, SelectedTags);
                    await _blogPostService.Update(viewModel);
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

            viewModel.SelectedCategories = viewModel.BlogPostCategories.Select(c => c.BlogCategoryId).AsEnumerable();
            ViewBag.SelectedCategories = new SelectList(await _blogCategoryService.GetAll(), "Id", "Title", viewModel.SelectedCategories);
            viewModel.SelectedTags = viewModel.BlogPostTags.Select(c => c.TagId).AsEnumerable();
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

            var viewModel = await _blogPostService.Get(Id);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int? Id, BlogPost viewModel)
        {
            await _blogPostService.Remove(Id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> Exists(int id)
        {
            return await _blogPostService.Exists(id);
        }
    }
}
