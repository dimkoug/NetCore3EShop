using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface IOrdersDetailService
    {
        Task<List<OrdersDetail>> GetAll();

        Task<OrdersDetail> Get(int? Id);

        Task<int> Add(OrdersDetail orderDetail);
        Task<int> Update(OrdersDetail orderDetail);

        Task Delete(int? Id);
    }
}
