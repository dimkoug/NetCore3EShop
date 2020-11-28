using ShopProject.Domain.Helpers;
using ShopProject.Domain.Models;
using ShopProject.Domain.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface ITagService
    {
        Task<List<Tag>> GetAll();
        Task<PageList<Tag>> GetAll(TagsResourceParameters parameters);

        Task<Tag> Get(int? Id);

        Task<Tag> Add(Tag viewModel);
        Task<Tag> Update(Tag viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);
    }
}
