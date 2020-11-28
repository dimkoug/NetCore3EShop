using Microsoft.AspNetCore.Http;
using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IBannerService
    {
        Task<List<Banner>> GetAll();

        Task<Banner> Get(int? Id);

        Task<Banner> Add(Banner viewModel);
        Task<Banner> Update(Banner viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);

        Task AddFile(Banner viewModel, IFormFile file);
    }
}
