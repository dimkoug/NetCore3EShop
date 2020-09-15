using Microsoft.AspNetCore.Http;
using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface IMediaService
    {
        Task<List<Media>> GetAll();

        Task<Media> Get(int? Id);

        Task<int> Add(Media document);
        Task<int> Update(Media document);

        Task Delete(int? Id);

        Task AddFile(Media document, IFormFile file);
        Task AddFiles(ICollection<IFormFile> Files);
        Task<bool> Exists(int? Id);
    }
}
