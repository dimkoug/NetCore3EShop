using Microsoft.AspNetCore.Http;
using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface IProductMediaService
    {
        Task Add(Products product, string[] items);

        Task AddFiles(Products product, ICollection<IFormFile> Files);

        Task Delete(int Id);
    }
}

