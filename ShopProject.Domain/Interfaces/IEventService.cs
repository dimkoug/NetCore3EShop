using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IEventService
    {
        Task<List<Event>> GetAll();

        Task<Event> Get(int? Id);

        Task<Event> Add(Event viewModel);
        Task<Event> Update(Event viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);
    }
}
