using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IOfferService
    {
        Task<List<Offer>> GetAll();

        Task<Offer> Get(int? Id);

        Task<Offer> Add(Offer viewModel);
        Task<Offer> Update(Offer viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);
    }
}
