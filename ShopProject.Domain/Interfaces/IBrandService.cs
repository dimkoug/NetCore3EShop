using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAll();

        Task<Brand> Get(int? Id);

        Task<Brand> Add(Brand viewModel);
        Task<Brand> Update(Brand viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);
    }
}
