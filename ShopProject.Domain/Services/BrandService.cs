using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopProject.Data;
using ShopProject.Domain.Models;
using ShopProject.Data.Entities;
using AutoMapper;
using ShopProject.Domain.Interfaces;

namespace ShopProject.Domain.Services
{
    public class BrandService :IBrandService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public BrandService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Brand> Add(Brand viewModel)
        {
            var model = _mapper.Map<BrandEntity>(viewModel);
            _context.Brands.Add(model);
            await _context.SaveChangesAsync();
            _mapper.Map(model, viewModel);
            return viewModel;
        }

        public async Task<bool> Exists(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            return await _context.Brands.AnyAsync(c => c.Id == Id);
        }

        public async Task<Brand> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Brands.Include(c=>c.Products).FirstOrDefaultAsync(c => c.Id == Id);
            var viewModel = _mapper.Map<Brand>(data);
            return viewModel;
        }

        public async Task<List<Brand>> GetAll()
        {
            var data = await _context.Brands.Include(c=>c.Products).ToListAsync();
            var viewList = _mapper.Map<List<BrandEntity>, List<Brand>>(data);
            return viewList;
        }

        public async Task Remove(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Brands.FirstOrDefaultAsync(c => c.Id == Id);
            _context.Brands.Remove(data);
            await _context.SaveChangesAsync();

        }

        public async Task<Brand> Update(Brand viewModel)
        {
            if (viewModel.Id == null)
            {
                throw new ArgumentNullException(nameof(viewModel.Id));
            }
            var data = await _context.Brands.FirstOrDefaultAsync(c => c.Id == viewModel.Id);
            _mapper.Map(viewModel, data);
            _context.Update(data);
            await _context.SaveChangesAsync();
            return viewModel;

        }
    }
}
