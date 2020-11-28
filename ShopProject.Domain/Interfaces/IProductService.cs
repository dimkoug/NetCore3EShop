using Microsoft.AspNetCore.Http;
using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();

        Task<Product> Get(int? Id);

        Task<Product> Add(Product viewModel);
        Task<Product> Update(Product viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);

        Task AddFile(Product viewModel, IFormFile file);
    }
}
