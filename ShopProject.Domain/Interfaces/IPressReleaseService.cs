using Microsoft.AspNetCore.Http;
using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IPressReleaseService
    {
        Task<List<PressRelease>> GetAll();

        Task<PressRelease> Get(int? Id);

        Task<PressRelease> Add(PressRelease viewModel);
        Task<PressRelease> Update(PressRelease viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);

        Task AddFile(PressRelease viewModel, IFormFile file);
    }
}
