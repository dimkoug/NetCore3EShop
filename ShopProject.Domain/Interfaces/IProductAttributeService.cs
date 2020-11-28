using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IProductAttributeService
    {
    
        Task<ProductAttribute> Get(int productId, int ProductAttributeId);

        Task Add(ProductAttribute productAttribute);

        Task Delete(int ProductId, int FeatureAttributeId);

        Task<bool> Exists(int productId, int FeatureAttributeId);
    }
}
