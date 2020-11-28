using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopProject.Data;
using ShopProject.Data.Entities;
using ShopProject.Domain.Interfaces;
using ShopProject.Domain.Models;

namespace ShopProject.Domain.Services
{
    public class EventLocationService : IEventLocationService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public EventLocationService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<EventLocation> Add(EventLocation viewModel)
        {
            var model = _mapper.Map<EventLocationEntity>(viewModel);
            _context.EventLocations.Add(model);
            await _context.SaveChangesAsync();
            _mapper.Map(model, viewModel);
            return viewModel;
        }

        public async Task<bool> Exists(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            return await _context.EventLocations.AnyAsync(c => c.Id == Id);
        }

        public async Task<EventLocation> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.EventLocations.FirstOrDefaultAsync(c => c.Id == Id);
            var viewModel = _mapper.Map<EventLocation>(data);
            return viewModel;
        }

        public async Task<List<EventLocation>> GetAll()
        {
            var data = await _context.EventLocations.ToListAsync();
            var viewList = _mapper.Map<List<EventLocationEntity>, List<EventLocation>>(data);
            return viewList;
        }

        public async Task Remove(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.EventLocations.FirstOrDefaultAsync(c => c.Id == Id);
            _context.EventLocations.Remove(data);
            await _context.SaveChangesAsync();

        }

        public async Task<EventLocation> Update(EventLocation viewModel)
        {
            if (viewModel.Id == null)
            {
                throw new ArgumentNullException(nameof(viewModel.Id));
            }
            var data = await _context.EventLocations.FirstOrDefaultAsync(c => c.Id == viewModel.Id);
            _mapper.Map(viewModel, data);
            _context.Update(data);
            await _context.SaveChangesAsync();
            return viewModel;

        }
    }
}
