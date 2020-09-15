using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopProject3.DataAccess;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Services
{
    public class ProductTagsService : IProductTagsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductTagsService(ApplicationDbContext context, IMapper mapper)
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
                    product.ProductTags.Add(new ProductTags { TagId = Convert.ToInt32(item), CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now });
                }
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var items = await _context.ProductTags.Where(c => c.ProductId == Id).ToListAsync();
            _context.ProductTags.RemoveRange(items);
            await Commit();
        }
    }
}
