using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IBlogPostService
    {
        Task<List<BlogPost>> GetAll();

        Task<BlogPost> Get(int? Id);

        Task<BlogPost> Add(BlogPost viewModel);
        Task<BlogPost> Update(BlogPost viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);
    }
}
