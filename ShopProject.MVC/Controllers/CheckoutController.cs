using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProject.Domain.Interfaces;
using ShopProject.Domain.Models;
using ShopProject.MVC.Helpers;
using ShopProject.MVC.Utilities;
using ShopProject.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.MVC.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ShoppingCartHelper _shoppingCart;

        private readonly IOrderService _orderService;



        public ShoppingCartVm shoppingCartViewModel;

        public CheckoutController(ShoppingCartHelper shoppingCart, IOrderService orderService)
        {
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
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
        public async Task<IActionResult> Create(Order viewModel)
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
                viewModel.OrderTotal = await _shoppingCart.GetShoppingCartTotal();
                await _orderService.Add(viewModel, shoppingCartItems);
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
