using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface IProductsService
    {
        Task<List<Products>> GetAll();

        Task<Products> Get(int? Id);

        Task<int> Add(Products product);
        Task<int> Update(Products product);

        Task Delete(int? Id);
        Task<bool> Exists(int? Id);
    }
}


