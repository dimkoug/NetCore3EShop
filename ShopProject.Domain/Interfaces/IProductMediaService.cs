using Microsoft.AspNetCore.Http;
using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IProductMediaService
    {
        Task Add(Product viewModel, ICollection<IFormFile> files);
        Task Delete(int Id);
    }
}
