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
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Add(Categories category)
        {
            var model = _mapper.Map<CategoriesEntity>(category);
            _context.Categories.Add(model);
            await Commit();
            return model.Id;
        }

        public async Task Delete(int? Id)
        {
            
            if(Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.Categories.Where(c => c.Id == Id).FirstOrDefaultAsync();
            _context.Categories.Remove(model);
            await Commit();
        }

        public async Task<Categories> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.Categories.Where(c => c.Id == Id).Include(c=>c.CategoryFeatures).Include(c=>c.ProductCategories).ThenInclude(c => c.Product).ThenInclude(c => c.Brand).Include(c=>c.Parent).ThenInclude(c=>c.InverseParent).FirstOrDefaultAsync();
            var data = _mapper.Map<Categories>(model);
            return data;
        }

        public async Task<List<Categories>> GetAll()
        {
            var model = await _context.Categories.Include(c => c.CategoryFeatures).Include(c => c.ProductCategories).ThenInclude(c=>c.Product).ThenInclude(c=>c.Brand).Include(c => c.Parent).ThenInclude(c => c.InverseParent).ToListAsync();
            var data = _mapper.Map<List<CategoriesEntity>, List<Categories>>(model);
            return data;
        }

        public async Task<int> Update(Categories category)
        {

            var model = await _context.Categories.Where(c => c.Id == category.Id).Include(c => c.CategoryFeatures).Include(c => c.ProductCategories).ThenInclude(c => c.Product).Include(c => c.Parent).ThenInclude(c => c.InverseParent).FirstOrDefaultAsync();
            var data = _mapper.Map(category, model);
            _context.Update(data);
            await Commit();
            return data.Id;
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
            return await _context.Categories.AnyAsync(a => a.Id == Id);
        }
    }
}
