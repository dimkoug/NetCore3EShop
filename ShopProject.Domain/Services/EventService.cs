using ShopProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopProject.Data;
using AutoMapper;
using ShopProject.Domain.Models;
using ShopProject.Data.Entities;

namespace ShopProject.Domain.Services
{
    public class EventService: IEventService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public EventService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Event> Add(Event viewModel)
        {
            var model = _mapper.Map<EventEntity>(viewModel);
            _context.Events.Add(model);
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
            return await _context.Events.AnyAsync(c => c.Id == Id);
        }

        public async Task<Event> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Events.Include(c=>c.EventTags).FirstOrDefaultAsync(c => c.Id == Id);
            var viewModel = _mapper.Map<Event>(data);
            return viewModel;
        }

        public async Task<List<Event>> GetAll()
        {
            var data = await _context.Events.Include(c => c.EventTags).ToListAsync();
            var viewList = _mapper.Map<List<EventEntity>, List<Event>>(data);
            return viewList;
        }

        public async Task Remove(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Events.FirstOrDefaultAsync(c => c.Id == Id);
            _context.Events.Remove(data);
            await _context.SaveChangesAsync();

        }

        public async Task<Event> Update(Event viewModel)
        {
            if (viewModel.Id == null)
            {
                throw new ArgumentNullException(nameof(viewModel.Id));
            }
            var data = await _context.Events.FirstOrDefaultAsync(c => c.Id == viewModel.Id);
            _mapper.Map(viewModel, data);
            _context.Update(data);
            await _context.SaveChangesAsync();
            return viewModel;

        }
    }
}
