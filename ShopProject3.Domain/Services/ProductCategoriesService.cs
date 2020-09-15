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
    public class ProductCategoriesService : IProductCategoriesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductCategoriesService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Add(Products product, string[] items)
        {
            if (items.Length > 0)
            {
                foreach (var item in items)
                {
                    product.ProductCategories.Add(new ProductCategories { CategoryId = Convert.ToInt32(item), CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now });
                }
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var items = await _context.ProductCategories.Where(c => c.ProductId == Id).ToListAsync();
            _context.ProductCategories.RemoveRange(items);
            await Commit();
        }

        public async Task<List<int>> Get(int? productId)
        {
            if(productId == null)
            {
                throw new ArgumentNullException(nameof(productId));
            }

            var items = await _context.ProductCategories.Include(c=>c.Product).ThenInclude(c=>c.Brand).Include(c => c.Category).Where(c => c.ProductId == productId).Select(c=>c.CategoryId).ToListAsync();
            return items;
        }
    }
}
