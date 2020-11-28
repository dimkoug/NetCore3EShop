using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IEventLocationService
    {
        Task<List<EventLocation>> GetAll();

        Task<EventLocation> Get(int? Id);

        Task<EventLocation> Add(EventLocation viewModel);
        Task<EventLocation> Update(EventLocation viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);
    }
}
