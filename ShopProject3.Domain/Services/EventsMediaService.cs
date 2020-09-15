using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShopProject3.DataAccess;
using ShopProject3.DataAccess.Entities;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Services
{
    public class EventsMediaService: IEventsMediaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EventsMediaService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<int> Add(EventsMedia eventsMedia)
        {
            var model = _mapper.Map<EventsMediaEntity>(eventsMedia);
            _context.EventsMedia.Add(model);
            await Commit();
            return model.Id;
            
        }

        public async Task Delete(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.EventsMedia.Where(c => c.Id == Id).FirstOrDefaultAsync();
            _context.EventsMedia.Remove(model);
            await Commit();
        }

        public async Task<bool> Exists(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            return await _context.EventsMedia.AnyAsync(a => a.Id == Id);
        }

        public async Task<EventsMedia> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.EventsMedia.Include(c=>c.Events).Where(e => e.Id == Id).FirstOrDefaultAsync();
            var model = _mapper.Map<EventsMedia>(data);
            return model;
        }

        public async Task<List<EventsMedia>> GetAll()
        {
            var data = await _context.EventsMedia.Include(c=>c.Events).ToListAsync();
            var model = _mapper.Map<List<EventsMediaEntity>, List<EventsMedia>>(data);
            return model;
        }

        public async Task<int> Update(EventsMedia eventsMedia)
        {
            if (eventsMedia.Id == null)
            {
                throw new ArgumentNullException(nameof(eventsMedia));
            }
            var data = await _context.EventsMedia.Where(c => c.Id == eventsMedia.Id).FirstOrDefaultAsync();
            var model = _mapper.Map(eventsMedia, data);
            _context.Update(model);
            await Commit();
            return model.Id;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddFile(EventsMedia model, IFormFile File)
        {
            if (File != null && File.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\uploaded_files", File.FileName);

                // If file with same name exists delete it
                if (System.IO.File.Exists(File.FileName))
                {
                    System.IO.File.Delete(File.FileName);
                }

                // Create new local file and copy contents of uploaded file
                //using (var localFile = System.IO.File.OpenWrite(filePath))
                using (var uploadedFile = new FileStream(filePath, FileMode.Create))
                {
                    await File.CopyToAsync(uploadedFile);
                }


                model.MediaPath = File.FileName;
            }
        }
    }
}
