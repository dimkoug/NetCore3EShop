using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface IPressReleasesService
    {
        Task<List<PressReleases>> GetAll();

        Task<PressReleases> Get(int? Id);

        Task<int> Add(PressReleases pressRelease);
        Task<int> Update(PressReleases pressRelease);

        Task Delete(int? Id);
        Task<bool> Exists(int? Id);
    }
}
