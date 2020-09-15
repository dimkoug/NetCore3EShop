using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopProject3.DataAccess;
using ShopProject3.DataAccess.Entities;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Services
{
    public class EventsService: IEventsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EventsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<int> Add(Events events)
        {
            var model = _mapper.Map<EventsEntity>(events);
            _context.Events.Add(model);
            await Commit();
            return model.Id;
        }

        public async Task Delete(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.Events.Where(c => c.Id == Id).FirstOrDefaultAsync();
            _context.Events.Remove(model);
            await Commit();
        }

        public async Task<bool> Exists(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            return await _context.Events.AnyAsync(a => a.Id == Id);
        }

        public async Task<Events> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Events.Include(c=>c.EventsMedia).Where(e => e.Id == Id).FirstOrDefaultAsync();
            var model = _mapper.Map<Events>(data);
            return model;
        }

        public async Task<List<Events>> GetAll()
        {
            var data = await _context.Events.Include(c => c.EventsMedia).ToListAsync();
            var model = _mapper.Map<List<EventsEntity>, List<Events>>(data);
            return model;
        }

        public async Task<int> Update(Events events)
        {
            if (events.Id == null)
            {
                throw new ArgumentNullException(nameof(events));
            }
            var data = await _context.Events.Where(c => c.Id == events.Id).FirstOrDefaultAsync();
            var model = _mapper.Map(events, data);
            _context.Update(model);
            await Commit();
            return model.Id;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
