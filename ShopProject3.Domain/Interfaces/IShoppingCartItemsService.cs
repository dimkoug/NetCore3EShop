using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface IShoppingCartItemsService
    {
        Task<List<ShoppingCartItems>> GetAll();

        Task<ShoppingCartItems> Get(int? Id);

        Task Add(ShoppingCartItems shoppingCartItem);
        Task Update(ShoppingCartItems shoppingCartItem);

        Task Delete(int? Id);
    }
}

