using Microsoft.AspNetCore.Http;
using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IShopCategoryService
    {
        Task<List<ShopCategory>> GetAll();

        Task<ShopCategory> Get(int? Id);

        Task<ShopCategory> Add(ShopCategory viewModel);
        Task<ShopCategory> Update(ShopCategory viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);

        Task AddFile(ShopCategory viewModel, IFormFile file);
    }
}
