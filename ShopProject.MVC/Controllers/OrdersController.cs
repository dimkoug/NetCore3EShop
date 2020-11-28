using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class OrdersController : Controller
    {
        private IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }



        public async Task<IActionResult> Index()
        {

            var controller = nameof(OrdersController).Split("Controller")[0];
            var data = await _orderService.GetAll();
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
            var controller = nameof(OrdersController).Split("Controller")[0];
            var html = "detail";

            var model = await _orderService.Get(Id);
            if (User.IsInRole(Roles.Owner) || User.Identity.IsAuthenticated)
            {
                html = "managedetail";
            }

            return View($"~/Views/{controller}/{html}.cshtml", model);
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
        public async Task<IActionResult> Create(Order viewModel)
        {
            if (User.IsInRole(Roles.Client) || !User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var Id = await _orderService.Add(viewModel);
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

            var viewModel = await _orderService.Get(Id);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int Id, Order viewModel)
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
                    await _orderService.Update(viewModel);
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

            var viewModel = await _orderService.Get(Id);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int? Id, Order viewModel)
        {
            await _orderService.Remove(Id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> Exists(int id)
        {
            return await _orderService.Exists(id);
        }
    }
}
