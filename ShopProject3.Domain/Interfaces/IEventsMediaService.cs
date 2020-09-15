using Microsoft.AspNetCore.Http;
using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Interfaces
{
    public interface IEventsMediaService
    {
        Task<List<EventsMedia>> GetAll();

        Task<EventsMedia> Get(int? Id);

        Task<int> Add(EventsMedia eventsMedia);
        Task<int> Update(EventsMedia eventsMedia);

        Task AddFile(EventsMedia eventsMedia, IFormFile file);

        Task Delete(int? Id);
        Task<bool> Exists(int? Id);
    }
}
