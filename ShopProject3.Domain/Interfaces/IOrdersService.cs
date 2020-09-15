using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface IOrdersService
    {
        Task<List<Orders>> GetAll();

        Task<Orders> Get(int? Id);

        Task<int> Add(Orders order,List<ShoppingCartItems> shoppingCartItems);
        Task<int> Update(Orders order);

        Task Delete(int? Id);
        Task<bool> Exists(int? Id);
    }
}

