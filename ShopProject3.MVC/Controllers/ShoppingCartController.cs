using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopProject3.Helpers;
using ShopProject3.MVC.ViewModels;

namespace ShopProject3.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCartHelper _shoppingCart;
        [TempData]
        public string Key { get; set; }
        [TempData]
        public string Message { get; set; }

        public ShoppingCartVm shoppingCartViewModel;

        public ShoppingCartController(ShoppingCartHelper shoppingCart)
        {
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
        }

        public async Task<IActionResult> Index()
        {
            var items = await _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items.ToList();
            shoppingCartViewModel = new ShoppingCartVm
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = await _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartViewModel);
        }

        public async Task<IActionResult> AddToShoppingCart(int productId, int p =0)
        {
            await _shoppingCart.AddToCart(productId);

            if (p == 1)
            {
                Key = "success";
                var callbackUrl = Url.Action(nameof(Index));
                Message = $"Product with id  {productId} added to <a href='{callbackUrl}'>Shopping cart</a>";
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveFromShoppingCart(int productId, int p=0)
        {

            await _shoppingCart.RemoveFromCart(productId);

            if (p == 1)
            {
                Key = "success";
                var callbackUrl = Url.Action(nameof(Index));
                Message = $"Product with id {productId} removed from  <a href='{callbackUrl}'>Shopping cart</a>";
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
