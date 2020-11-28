using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IFeatureService
    {
        Task<List<Feature>> GetAll();

        Task<Feature> Get(int? Id);

        Task<Feature> Add(Feature viewModel);
        Task<Feature> Update(Feature viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);
    }
}
