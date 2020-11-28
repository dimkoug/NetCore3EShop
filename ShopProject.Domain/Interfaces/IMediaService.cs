using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IMediaService
    {
        Task<List<Media>> GetAll();

        Task<Media> Get(int? Id);

        Task<Media> Add(Media viewModel);
        Task<Media> Update(Media viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);
    }
}
