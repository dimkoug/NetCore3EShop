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
    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Add(Products product)
        {
            var model = _mapper.Map<ProductsEntity>(product);
            _context.Products.Add(model);
            await Commit();
            return model.Id;
        }

        public async Task Delete(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.Products.Where(c => c.Id == Id).FirstOrDefaultAsync();
            _context.Products.Remove(model);
            await Commit();
        }

        public async Task<Products> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.Products.Include(c => c.Brand).Include(c => c.ProductMedia).ThenInclude(c=>c.Media).Include(c=>c.ProductTags).ThenInclude(c=>c.Tag).Include(c=>c.ProductFeatureAttributes).ThenInclude(c=>c.FeatureAttribute).ThenInclude(c => c.Feature).Include(c=>c.ProductCategories).Include(c=>c.Parent).Include(f=>f.Children).Where(c => c.Id == Id).FirstOrDefaultAsync();
            var data = _mapper.Map<Products>(model);
            return data;
        }

        public async Task<List<Products>> GetAll()
        {
            var model = await _context.Products.Include(c=>c.Brand).Include(c => c.ProductMedia).ThenInclude(c => c.Media).Include(c => c.ProductTags).ThenInclude(c => c.Tag).Include(c => c.ProductFeatureAttributes).ThenInclude(c => c.FeatureAttribute).ThenInclude(c=>c.Feature).Include(c => c.ProductTags).Include(c => c.ProductFeatureAttributes).Include(c => c.ProductCategories).Include(c => c.Parent).Include(f => f.Children).ToListAsync();
            var data = _mapper.Map<List<ProductsEntity>, List<Products>>(model);
            return data;
        }

        public async Task<int> Update(Products product)
        {
            var model = await _context.Products.Where(c => c.Id == product.Id).FirstOrDefaultAsync();
            var data = _mapper.Map(product, model);
            _context.Update(model);
            await Commit();
            return model.Id;
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
            return await _context.Products.AnyAsync(a => a.Id == Id);
        }
    }
}
