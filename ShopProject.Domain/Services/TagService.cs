using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopProject.Data;
using ShopProject.Data.Entities;
using ShopProject.Domain.Helpers;
using ShopProject.Domain.Interfaces;
using ShopProject.Domain.Models;
using ShopProject.Domain.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Services
{
    public class TagService: ITagService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public TagService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Tag> Add(Tag viewModel)
        {
            var model = _mapper.Map<TagEntity>(viewModel);
            _context.Tags.Add(model);
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
            return await _context.Tags.AnyAsync(c => c.Id == Id);
        }

        public async Task<Tag> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Tags.FirstOrDefaultAsync(c => c.Id == Id);
            var viewModel = _mapper.Map<Tag>(data);
            return viewModel;
        }

        public async Task<List<Tag>> GetAll()
        {
            var data = await _context.Tags.ToListAsync();
            var viewList = _mapper.Map<List<TagEntity>, List<Tag>>(data);
            return viewList;
        }

        public async Task<PageList<Tag>> GetAll(TagsResourceParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            var model = await _context.Tags.Include(c => c.Products).ThenInclude(c => c.Product).ToListAsync();

            var data = _mapper.Map<List<TagEntity>, List<Tag>>(model).AsQueryable();
            return PageList<Tag>.Create(data, parameters.PageNumber, parameters.PageSize);
        }

        public async Task Remove(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Tags.FirstOrDefaultAsync(c => c.Id == Id);
            _context.Tags.Remove(data);
            await _context.SaveChangesAsync();
        }

        public async Task<Tag> Update(Tag viewModel)
        {
            if (viewModel.Id == null)
            {
                throw new ArgumentNullException(nameof(viewModel.Id));
            }
            var data = await _context.Tags.FirstOrDefaultAsync(c => c.Id == viewModel.Id);
            _mapper.Map(viewModel, data);
            _context.Update(data);
            await _context.SaveChangesAsync();
            return viewModel;
        }
    }
}
