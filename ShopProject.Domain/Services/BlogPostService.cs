using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopProject.Data;
using ShopProject.Data.Entities;
using ShopProject.Domain.Interfaces;
using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Services
{
    public class BlogPostService : IBlogPostService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public BlogPostService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BlogPost> Add(BlogPost viewModel)
        {
            var model = _mapper.Map<BlogPostEntity>(viewModel);
            _context.BlogPosts.Add(model);
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
            return await _context.BlogPosts.AnyAsync(c => c.Id == Id);
        }

        public async Task<BlogPost> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.BlogPosts.Include(c=>c.BlogPostCategories).Include(c=>c.PostTags).FirstOrDefaultAsync(c => c.Id == Id);
            var viewModel = _mapper.Map<BlogPost>(data);
            return viewModel;
        }

        public async Task<List<BlogPost>> GetAll()
        {
            var data = await _context.BlogPosts.Include(c => c.BlogPostCategories).Include(c => c.PostTags).ToListAsync();
            var viewList = _mapper.Map<List<BlogPostEntity>, List<BlogPost>>(data);
            return viewList;
        }

        public async Task Remove(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.BlogPosts.FirstOrDefaultAsync(c => c.Id == Id);
            _context.BlogPosts.Remove(data);
            await _context.SaveChangesAsync();

        }

        public async Task<BlogPost> Update(BlogPost viewModel)
        {
            if (viewModel.Id == null)
            {
                throw new ArgumentNullException(nameof(viewModel.Id));
            }
            var data = await _context.BlogPosts.FirstOrDefaultAsync(c => c.Id == viewModel.Id);
            _mapper.Map(viewModel, data);
            _context.Update(data);
            await _context.SaveChangesAsync();
            return viewModel;

        }
    }
}
