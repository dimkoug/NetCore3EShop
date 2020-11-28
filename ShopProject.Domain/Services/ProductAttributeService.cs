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
    public class ProductAttributeService: IProductAttributeService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public ProductAttributeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Add(ProductAttribute productAttribute)
        {
            var model = _mapper.Map<ProductAttributeEntity>(productAttribute);
            _context.ProductAttributes.Add(model);
            await _context.SaveChangesAsync();


        }
             
        
      
        public async Task<bool> Exists(int productId, int FeatureAttributeId)
        {
            var exists = await _context.ProductAttributes.AnyAsync(c => c.ProductsId == productId && c.FeatureAttributesId == FeatureAttributeId);
            return exists;
        }

        public async Task<ProductAttribute> Get(int productId, int FeatureAttributeId)
        {
            var data = await _context.ProductAttributes.Include(c => c.Products).Include(c => c.FeatureAttributes).ThenInclude(c => c.Feature).Where(c => c.ProductsId == productId && c.FeatureAttributesId == FeatureAttributeId).FirstOrDefaultAsync();
            var model = _mapper.Map<ProductAttribute>(data);
            return model;

        }

        public async Task Delete(int ProductId, int FeatureAttributeId)
        {
            var data = await _context.ProductAttributes.Include(c => c.Products).Include(c => c.FeatureAttributes).ThenInclude(c => c.Feature).Where(c => c.ProductsId == ProductId && c.FeatureAttributesId == FeatureAttributeId).FirstOrDefaultAsync();
            _context.ProductAttributes.Remove(data);
            await _context.SaveChangesAsync();
        }
    }
}
