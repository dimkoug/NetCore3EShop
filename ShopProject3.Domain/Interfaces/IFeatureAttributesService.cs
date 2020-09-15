using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface IFeatureAttributesService
    {
        Task<List<FeatureAttributes>> GetAll();

        Task<FeatureAttributes> Get(int? Id);

        Task<int> Add(FeatureAttributes featureAttribute);
        Task<int> Update(FeatureAttributes featureAttribute);

        Task Delete(int? Id);
    }
}
