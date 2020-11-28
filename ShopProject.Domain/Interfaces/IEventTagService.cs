using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IEventTagService
    {
        Task Add(Event viewModel, string[] items);
        Task Delete(int Id);
    }
}
