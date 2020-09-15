using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface IOffersService
    {
        Task<List<Offers>> GetAll();

        Task<Offers> Get(int? Id);

        Task<int> Add(Offers offer);
        Task<int> Update(Offers offer);

        Task Delete(int? Id);
        Task<bool> Exists(int? Id);
    }
}
