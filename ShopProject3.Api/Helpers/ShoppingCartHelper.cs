using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject3.API.Helpers
{
    public class ShoppingCartHelper
    {
        public IShoppingCartService _shoppingCartService;
        private readonly IServiceProvider _services;

        public string SessionCartId { get; set; }

        public List<ShoppingCartItems> ShoppingCartItems { get; set; }

        private ShoppingCartHelper(IShoppingCartService shoppingCartService, IServiceProvider services)
        {
            _shoppingCartService = shoppingCartService ?? throw new ArgumentException(nameof(shoppingCartService));
            _services = services ?? throw new ArgumentException(nameof(services));
        }

        public static ShoppingCartHelper GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var shoppingCartService = services.GetService<IShoppingCartService>();
            var serviceProvider = services.GetService<IServiceProvider>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCartHelper(shoppingCartService, serviceProvider) { SessionCartId = cartId };
        }

        public async Task AddToCart(int productId)
        {
            await _shoppingCartService.AddToCart(SessionCartId, productId);
        }

        public async Task<int> RemoveFromCart(int productId)
        {
            return await _shoppingCartService.RemoveFromCart(SessionCartId, productId);
        }

        public async Task<List<ShoppingCartItems>> GetShoppingCartItems()
        {

            var data = await _shoppingCartService.GetShoppingCartItems(SessionCartId);
            return ShoppingCartItems ?? data;


        }

        public async Task ClearCart(string SessionCartId)
        {
            await _shoppingCartService.ClearCart(SessionCartId);
        }

        public async Task<decimal> GetShoppingCartTotal()
        {
            ISession session = _services.GetRequiredService<IHttpContextAccessor>()?
.HttpContext.Session;
            return await _shoppingCartService.GetShoppingCartTotal(SessionCartId);
        }

    }
}
