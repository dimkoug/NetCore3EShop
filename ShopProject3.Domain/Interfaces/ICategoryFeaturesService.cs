using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface ICategoryFeaturesService
    {
        Task<List<CategoryFeatures>> GetAll();
        
        Task Add(Features feature, string[] items);
        Task Delete(int Id);
    }
}
