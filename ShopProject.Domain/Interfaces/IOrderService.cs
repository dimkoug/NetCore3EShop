using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAll();

        Task<Order> Get(int? Id);

        Task<Order> Add(Order viewModel);
        Task<int> Add(Order order, List<ShoppingCartItem> shoppingCartItems);
        Task<Order> Update(Order viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);
    }
}
