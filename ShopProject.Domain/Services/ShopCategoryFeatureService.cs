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
    public class ShopCategoryFeatureService: IShopCategoryFeatureService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public ShopCategoryFeatureService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Add(Feature viewModel, string[] items)
        {
            if (items.Length > 0)
            {
                foreach (var item in items)
                {
                    viewModel.ShopCategoryFeatures.Add(new ShopCategoryFeature { ShopCategoryId = Convert.ToInt32(item), CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now });
                }
            }
        }

        public async Task Delete(int Id)
        {
            var items = await _context.ShopCategoryFeatures.Where(c => c.ProductFeatureId == Id).ToListAsync();
            _context.ShopCategoryFeatures.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ShopCategoryFeature>> GetAll()
        {
            var entities = await _context.ShopCategoryFeatures.Include(c => c.ProductFeature).ThenInclude(c=>c.FeatureAttributes).Include(c => c.ShopCategory).ToListAsync();
            var data = _mapper.Map<List<ShopCategoryFeatureEntity>, List<ShopCategoryFeature>>(entities);
            return data;
        }
    }
}
