using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Models;
using ShopProject3.Helpers;
using ShopProject3.MVC.Utilities;
using ShopProject3.MVC.ViewModels;

namespace ShopProject3.MVC.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ShoppingCartHelper _shoppingCart;

        private readonly IOrdersService _ordersService;



        public ShoppingCartVm shoppingCartViewModel;

        public CheckoutController(ShoppingCartHelper shoppingCart, IOrdersService ordersService)
        {
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
            _ordersService = ordersService ?? throw new ArgumentNullException(nameof(ordersService));
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            if (User.IsInRole(Roles.Client))
            {
                return View();
            }
            return View("~/Views/Checkout/ManageCreate.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Orders viewModel)
        {
            var items = await _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            shoppingCartViewModel = new ShoppingCartVm
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = await _shoppingCart.GetShoppingCartTotal()
            };

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty.");
            }

            if (ModelState.IsValid)
            {
                var shoppingCartItems = _shoppingCart.ShoppingCartItems;
                viewModel.Total = await _shoppingCart.GetShoppingCartTotal();
                await _ordersService.Add(viewModel, shoppingCartItems);
                await _shoppingCart.ClearCart(HttpContext.Session.GetString("CartId"));
                return RedirectToAction(nameof(Index));
            }

            if (User.IsInRole(Roles.Client))
            {
                return View(viewModel);
            }
            return View("~/Views/Checkout/ManageCreate.cshtml", viewModel);
        }
    }
}
