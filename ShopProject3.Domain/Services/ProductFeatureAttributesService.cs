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
    public class ProductFeatureAttributesService : IProductFeatureAttributesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductFeatureAttributesService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Add(ProductFeatureAttributes productFeatureAttribute)
        {
            var model = _mapper.Map<ProductFeatureAttributesEntity>(productFeatureAttribute);
            _context.ProductFeatureAttributes.Add(model);
            await Commit();
            

        }

        public async Task Delete(int? Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductFeatureAttributes> Get(int? Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductFeatureAttributes>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Update(ProductFeatureAttributes productFeatureAttribute)
        {
            throw new NotImplementedException();
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int productId, int FeatureAttributeId)
        {
            var exists = await _context.ProductFeatureAttributes.AnyAsync(c => c.ProductId == productId && c.FeatureAttributeId == FeatureAttributeId);
            return exists;
        }

        public async Task<ProductFeatureAttributes> Get(int productId, int FeatureAttributeId)
        {
            var data = await _context.ProductFeatureAttributes.Include(c=>c.Product).Include(c=>c.FeatureAttribute).ThenInclude(c=>c.Feature).Where(c => c.ProductId == productId && c.FeatureAttributeId == FeatureAttributeId).FirstOrDefaultAsync();
            var model = _mapper.Map<ProductFeatureAttributes>(data);
            return model;

        }

        public async Task Delete(int ProductId, int FeatureAttributeId)
        {
            var data = await _context.ProductFeatureAttributes.Include(c => c.Product).Include(c => c.FeatureAttribute).ThenInclude(c => c.Feature).Where(c => c.ProductId == ProductId && c.FeatureAttributeId == FeatureAttributeId).FirstOrDefaultAsync();
            _context.ProductFeatureAttributes.Remove(data);
            await _context.SaveChangesAsync();
        }
    }
}
