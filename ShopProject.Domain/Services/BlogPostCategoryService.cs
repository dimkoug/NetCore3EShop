using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopProject.Data;
using ShopProject.Domain.Interfaces;
using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Services
{
    public class BlogPostCategoryService : IBlogPostCategoryService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public BlogPostCategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Add(BlogPost viewModel, string[] items)
        {
            if (items.Length > 0)
            {
                foreach (var item in items)
                {
                    viewModel.BlogPostCategories.Add(new BlogPostCategory { BlogCategoryId = Convert.ToInt32(item), CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now });
                }
            }
        }

        public async Task Delete(int Id)
        {
            var items = await _context.BlogPostCategories.Where(c => c.BlogPostId == Id).ToListAsync();
            _context.BlogPostCategories.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
