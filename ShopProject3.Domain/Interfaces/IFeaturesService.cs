using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface IFeaturesService
    {
        Task<List<Features>> GetAll();

        Task<Features> Get(int? Id);

        Task<int> Add(Features feature);
        Task<int> Update(Features feature);

        Task Delete(int? Id);
        Task<bool> Exists(int? Id);
    }
}
