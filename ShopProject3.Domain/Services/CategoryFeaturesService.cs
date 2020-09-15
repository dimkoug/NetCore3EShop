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
    public class CategoryFeaturesService : ICategoryFeaturesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryFeaturesService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Add(Features feature, string[] items)
        {
            if (items.Length > 0)
            {
                foreach(var item in items)
                {
                    feature.CategoryFeatures.Add(new CategoryFeatures { CategoryId = Convert.ToInt32(item), CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now });
                }
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var items = await _context.CategoryFeatures.Where(c => c.FeatureId == Id).ToListAsync();
            _context.CategoryFeatures.RemoveRange(items);
            await Commit();
        }


        public async Task<List<CategoryFeatures>> GetAll()
        {
            var entities = await _context.CategoryFeatures.Include(c => c.Feature).Include(c=>c.Category).ToListAsync();
            var data = _mapper.Map<List<CategoryFeaturesEntity>, List<CategoryFeatures>>(entities);
            return data;
        }
    }
}
