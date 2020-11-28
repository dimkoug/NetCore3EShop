using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IOfferDetailService
    {
        Task<List<OfferDetail>> GetAll();

        Task<OfferDetail> Get(int? Id);

        Task<OfferDetail> Add(OfferDetail viewModel);
        Task<OfferDetail> Update(OfferDetail viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);
    }
}
