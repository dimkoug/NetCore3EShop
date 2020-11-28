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
    public class FeatureAttributeService: IFeatureAttributeService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public FeatureAttributeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<FeatureAttribute> Add(FeatureAttribute viewModel)
        {
            var model = _mapper.Map<FeatureAttributeEntity>(viewModel);
            _context.FeatureAttributes.Add(model);
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
            return await _context.FeatureAttributes.AnyAsync(c => c.Id == Id);
        }

        public async Task<FeatureAttribute> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.FeatureAttributes.FirstOrDefaultAsync(c => c.Id == Id);
            var viewModel = _mapper.Map<FeatureAttribute>(data);
            return viewModel;
        }

        public async Task<List<FeatureAttribute>> GetAll()
        {
            var data = await _context.FeatureAttributes.Include(c=>c.Feature).ToListAsync();
            var viewList = _mapper.Map<List<FeatureAttributeEntity>, List<FeatureAttribute>>(data);
            return viewList;
        }

        public async Task Remove(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.FeatureAttributes.FirstOrDefaultAsync(c => c.Id == Id);
            _context.FeatureAttributes.Remove(data);
            await _context.SaveChangesAsync();
        }

        public async Task<FeatureAttribute> Update(FeatureAttribute viewModel)
        {
            if (viewModel.Id == null)
            {
                throw new ArgumentNullException(nameof(viewModel.Id));
            }
            var data = await _context.FeatureAttributes.FirstOrDefaultAsync(c => c.Id == viewModel.Id);
            _mapper.Map(viewModel, data);
            _context.Update(data);
            await _context.SaveChangesAsync();
            return viewModel;
        }
    }
}
