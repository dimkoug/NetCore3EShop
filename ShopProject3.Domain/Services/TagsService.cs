using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopProject3.DataAccess;
using ShopProject3.DataAccess.Entities;
using ShopProject3.Domain.Helpers;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Models;
using ShopProject3.Domain.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Services
{
    public class TagsService : ITagsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TagsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Tags> Add(Tags tag)
        {
            var model = _mapper.Map<TagsEntity>(tag);
            _context.Tags.Add(model);
            await Commit();
            var data = _mapper.Map<Tags>(model);
            return data;
        }

        public async Task Delete(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.Tags.Where(c => c.Id == Id).FirstOrDefaultAsync();
            _context.Tags.Remove(model);
            await Commit();
        }

        public async Task<Tags> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.Tags.Include(c => c.ProductTags).ThenInclude(c => c.Product).Where(c => c.Id == Id).FirstOrDefaultAsync();
            var data = _mapper.Map<Tags>(model);
            return data;
        }

        public async Task<List<Tags>> GetAll()
        {
            var model = await _context.Tags.Include(c => c.ProductTags).ThenInclude(c => c.Product).ToListAsync();
            var data = _mapper.Map<List<TagsEntity>, List<Tags>>(model);
            return data;
        }

        public async Task<Tags> Update(Tags tag)
        {
            var model = await _context.Tags.Where(c => c.Id == tag.Id).FirstOrDefaultAsync();
            var data = _mapper.Map(tag, model);
            _context.Update(model);
            await Commit();
            var saved_data = _mapper.Map<Tags>(model);
            return saved_data;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            return await _context.Tags.AnyAsync(a => a.Id == Id);
        }

        public async Task<PageList<Tags>> GetAll(TagsResourceParameters parameters)
        {
            if(parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            var model = await _context.Tags.Include(c => c.ProductTags).ThenInclude(c => c.Product).ToListAsync();

            var data = _mapper.Map<List<TagsEntity>, List<Tags>>(model).AsQueryable();
            return PageList<Tags>.Create(data,parameters.PageNumber,parameters.PageSize);
        }
    }
}
