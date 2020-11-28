using Microsoft.AspNetCore.Mvc;
using ShopProject.Domain.Models;
using ShopProject.MVC.Helpers;
using ShopProject.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCartHelper _shoppingCart;

        public ShoppingCartSummary(ShoppingCartHelper shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var items = await _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartVm
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = await _shoppingCart.GetShoppingCartTotal()

            };
            return View(shoppingCartViewModel);
        }
    }
}
