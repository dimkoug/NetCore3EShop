using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface IEventsService
    {
        Task<List<Events>> GetAll();

        Task<Events> Get(int? Id);

        Task<int> Add(Events events);
        Task<int> Update(Events events);

        Task Delete(int? Id);
        Task<bool> Exists(int? Id);
    }
}
