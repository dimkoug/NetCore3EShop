using Microsoft.AspNetCore.Mvc;
using ShopProject3.Domain.Models;
using ShopProject3.Helpers;
using ShopProject3.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject3.Components
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

            var items =  await _shoppingCart.GetShoppingCartItems();
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
