using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface ICategoriesService
    {
        Task<List<Categories>> GetAll();

        Task<Categories> Get(int? Id);

        Task<int> Add(Categories category);
        Task<int> Update(Categories category);

        Task Delete(int? Id);
        Task<bool> Exists(int? Id);
    }
}
