using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface IShoppingCartService
    {

        public Task AddToCart(string SessionCartId, int productId);
        public Task<int> RemoveFromCart(string SessionCartId, int productId);

        public Task<List<ShoppingCartItems>> GetShoppingCartItems(string SessionCartId);

        public Task ClearCart(string? SessionCartId);
        public Task<decimal> GetShoppingCartTotal(string SessionCartId);
    }
}
