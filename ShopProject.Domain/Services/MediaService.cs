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
    public class MediaService: IMediaService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public MediaService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Media> Add(Media viewModel)
        {
            var model = _mapper.Map<MediaEntity>(viewModel);
            _context.Media.Add(model);
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
            return await _context.Media.AnyAsync(c => c.Id == Id);
        }

        public async Task<Media> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Media.FirstOrDefaultAsync(c => c.Id == Id);
            var viewModel = _mapper.Map<Media>(data);
            return viewModel;
        }

        public async Task<List<Media>> GetAll()
        {
            var data = await _context.Media.ToListAsync();
            var viewList = _mapper.Map<List<MediaEntity>, List<Media>>(data);
            return viewList;
        }

        public async Task Remove(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Media.FirstOrDefaultAsync(c => c.Id == Id);
            _context.Media.Remove(data);
            await _context.SaveChangesAsync();

        }

        public async Task<Media> Update(Media viewModel)
        {
            if (viewModel.Id == null)
            {
                throw new ArgumentNullException(nameof(viewModel.Id));
            }
            var data = await _context.Media.FirstOrDefaultAsync(c => c.Id == viewModel.Id);
            _mapper.Map(viewModel, data);
            _context.Update(data);
            await _context.SaveChangesAsync();
            return viewModel;

        }
    }
}
