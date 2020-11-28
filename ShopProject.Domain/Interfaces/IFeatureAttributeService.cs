using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopProject.Domain.Models;

namespace ShopProject.Domain.Interfaces
{
    public interface IFeatureAttributeService
    {
        Task<List<FeatureAttribute>> GetAll();

        Task<FeatureAttribute> Get(int? Id);

        Task<FeatureAttribute> Add(FeatureAttribute viewModel);
        Task<FeatureAttribute> Update(FeatureAttribute viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);
    }
}
