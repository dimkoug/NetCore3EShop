using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IOrderDetailService
    {
        Task<List<OrderDetail>> GetAll();

        Task<OrderDetail> Get(int? Id);

        Task<OrderDetail> Add(OrderDetail viewModel);
        Task<OrderDetail> Update(OrderDetail viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);
    }
}
