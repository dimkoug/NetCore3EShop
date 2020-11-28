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
    public class FeatureService: IFeatureService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public FeatureService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Feature> Add(Feature viewModel)
        {
            var model = _mapper.Map<FeatureEntity>(viewModel);
            _context.Features.Add(model);
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
            return await _context.Features.AnyAsync(c => c.Id == Id);
        }

        public async Task<Feature> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Features.Include(c=>c.ShopCategoryFeatures).FirstOrDefaultAsync(c => c.Id == Id);
            var viewModel = _mapper.Map<Feature>(data);
            return viewModel;
        }

        public async Task<List<Feature>> GetAll()
        {
            var data = await _context.Features.Include(c => c.ShopCategoryFeatures).ToListAsync();
            var viewList = _mapper.Map<List<FeatureEntity>, List<Feature>>(data);
            return viewList;
        }

        public async Task Remove(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Features.FirstOrDefaultAsync(c => c.Id == Id);
            _context.Features.Remove(data);
            await _context.SaveChangesAsync();
        }

        public async Task<Feature> Update(Feature viewModel)
        {
            if (viewModel.Id == null)
            {
                throw new ArgumentNullException(nameof(viewModel.Id));
            }
            var data = await _context.Features.FirstOrDefaultAsync(c => c.Id == viewModel.Id);
            _mapper.Map(viewModel, data);
            _context.Update(data);
            await _context.SaveChangesAsync();
            return viewModel;
        }
    }
}
