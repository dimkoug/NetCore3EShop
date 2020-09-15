using Microsoft.AspNetCore.Http;
using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface IBannersService
    {
        Task<List<Banners>> GetAll();

        Task<Banners> Get(int? Id);

        Task<int> Add(Banners banner);
        Task<int> Update(Banners banner);

        Task AddFile(Banners document, IFormFile file);

        Task Delete(int? Id);
        Task<bool> Exists(int? Id);
    }
}
