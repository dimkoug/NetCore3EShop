using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IShoppingCartService
    {
        public Task AddToCart(string SessionCartId, int productId);
        public Task<int> RemoveFromCart(string SessionCartId, int productId);

        public Task<List<ShoppingCartItem>> GetShoppingCartItems(string SessionCartId);

        public Task ClearCart(string? SessionCartId);
        public Task<decimal> GetShoppingCartTotal(string SessionCartId);
    }
}
