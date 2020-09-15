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
    public class FeatureService : IFeaturesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FeatureService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Add(Features feature)
        {
            var model = _mapper.Map<FeaturesEntity>(feature);
            _context.Features.Add(model);
            await Commit();
            return model.Id;
        }

        public async Task Delete(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.Features.Where(c => c.Id == Id).FirstOrDefaultAsync();
            _context.Features.Remove(model);
            await Commit();
        }

        public async Task<Features> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.Features.Include(c => c.FeatureAttributes).Include(c=>c.CategoryFeatures).Where(c => c.Id == Id).FirstOrDefaultAsync();
            var data = _mapper.Map<Features>(model);
            return data;
        }

        public async Task<List<Features>> GetAll()
        {
            var model = await _context.Features.Include(c => c.FeatureAttributes).Include(c => c.CategoryFeatures).ToListAsync();
            var data = _mapper.Map<List<FeaturesEntity>, List<Features>>(model);
            return data;
        }

        public async Task<int> Update(Features feature)
        {
            var model = await _context.Features.Where(c => c.Id == feature.Id).FirstOrDefaultAsync();
            var data = _mapper.Map(feature, model);
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
            return await _context.Features.AnyAsync(a => a.Id == Id);
        }
    }
}
