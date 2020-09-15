using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopProject3.DataAccess;
using ShopProject3.DataAccess.Entities;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Services
{
    public class BrandsService : IBrandsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BrandsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Add(Brands brand)
        {
            var model = _mapper.Map<BrandsEntity>(brand);
            _context.Brands.Add(model);
            await Commit();
            return model.Id;
        }

        public async Task Delete(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.Brands.Where(c=> c.Id==Id).FirstOrDefaultAsync();
            _context.Brands.Remove(model);
            await Commit();
        }

        public async Task<Brands> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Brands.Where(e => e.Id == Id).Include(c => c.Products).FirstOrDefaultAsync();
            var model = _mapper.Map<Brands>(data);
            return model;
        }

        public async Task<List<Brands>> GetAll()
        {
            var data = await _context.Brands.Include(c => c.Products).ToListAsync();
            var model = _mapper.Map<List<BrandsEntity>, List<Brands>>(data);
            return model;
        }

        public async Task<int> Update(Brands brand)
        {
            if (brand.Id == null)
            {
                throw new ArgumentNullException(nameof(brand));
            }
            var data = await _context.Brands.Where(c => c.Id == brand.Id).FirstOrDefaultAsync();
            var model = _mapper.Map(brand, data);
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
            return await _context.Brands.AnyAsync(a => a.Id == Id);
        }
    }
}
