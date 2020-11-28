using Microsoft.AspNetCore.Http;
using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IBlogCategoryService
    {
        Task<List<BlogCategory>> GetAll();

        Task<BlogCategory> Get(int? Id);

        Task<BlogCategory> Add(BlogCategory viewModel);
        Task<BlogCategory> Update(BlogCategory viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);

        Task AddFile(BlogCategory viewModel, IFormFile file);
    }
}
