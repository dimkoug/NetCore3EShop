using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface IBrandsService
    {
        Task<List<Brands>> GetAll();

        Task<Brands> Get(int? Id);

        Task<int> Add(Brands brand);
        Task<int> Update(Brands Brand);

        Task Delete(int? Id);
        Task<bool> Exists(int? Id);
    }
}
