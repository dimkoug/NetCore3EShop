using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface IProductFeatureAttributesService
    {
        Task<List<ProductFeatureAttributes>> GetAll();

        Task<ProductFeatureAttributes> Get(int? Id);

        Task<ProductFeatureAttributes> Get(int productId, int FeatureAttributeId);

        Task Add(ProductFeatureAttributes productFeatureAttribute);
        Task Update(ProductFeatureAttributes productFeatureAttribute);

        Task Delete(int? Id);

        Task Delete(int ProductId, int FeatureAttributeId);

        Task<bool> Exists(int productId, int FeatureAttributeId);
    }
}

