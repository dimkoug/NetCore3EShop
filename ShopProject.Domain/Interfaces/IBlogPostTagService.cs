using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IBlogPostTagService
    {
        Task Add(BlogPost viewModel, string[] items);
        Task Delete(int Id);
    }
}
