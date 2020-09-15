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
    public class FeatureAttributesService : IFeatureAttributesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FeatureAttributesService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Add(FeatureAttributes featureAttribute)
        {
            var model = _mapper.Map<FeatureAttributesEntity>(featureAttribute);
            _context.FeatureAttributes.Add(model);
            await Commit();
            return model.Id;
        }

        public async Task Delete(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.FeatureAttributes.Where(c => c.Id == Id).FirstOrDefaultAsync();
            _context.FeatureAttributes.Remove(model);
            await Commit();
        }

        public async Task<FeatureAttributes> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.FeatureAttributes.Include(c => c.Feature).Where(c => c.Id == Id).FirstOrDefaultAsync();
            var data = _mapper.Map<FeatureAttributes>(model);
            return data;
        }

        public async Task<List<FeatureAttributes>> GetAll()
        {
            var model = await _context.FeatureAttributes.Include(c => c.Feature).ToListAsync();
            var data = _mapper.Map<List<FeatureAttributesEntity>, List<FeatureAttributes>>(model);
            return data;
        }

        public async Task<int> Update(FeatureAttributes featureAttribute)
        {
            var model = await _context.FeatureAttributes.Include(c => c.Feature).Where(c => c.Id == featureAttribute.Id).FirstOrDefaultAsync();
            var data = _mapper.Map(featureAttribute, model);
            _context.Update(model);
            await Commit();
            return model.Id;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
