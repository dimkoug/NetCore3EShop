using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface IOffersDetailService
    {
        Task<List<OffersDetail>> GetAll();

        Task<OffersDetail> Get(int? Id);

        Task<int> Add(OffersDetail offersDetail);
        Task<int> Update(OffersDetail offersDetail);

        Task Delete(int? Id);
        Task<bool> Exists(int? Id);
    }
}
