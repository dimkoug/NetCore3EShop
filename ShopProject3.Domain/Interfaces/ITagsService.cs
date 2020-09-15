using ShopProject3.Domain.Helpers;
using ShopProject3.Domain.Models;
using ShopProject3.Domain.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface ITagsService
    {
        Task<List<Tags>> GetAll();

        Task<PageList<Tags>> GetAll(TagsResourceParameters parameters);

        Task<Tags> Get(int? Id);

        Task<Tags> Add(Tags tag);
        Task<Tags> Update(Tags tag);

        Task Delete(int? Id);
        Task<bool> Exists(int? Id);
    }
}

