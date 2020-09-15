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
    public class OffersDetailService: IOffersDetailService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OffersDetailService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<int> Add(OffersDetail offersDetail)
        {
            var model = _mapper.Map<OffersDetailEntity>(offersDetail);
            _context.OffersDetail.Add(model);
            await Commit();
            return model.Id;
        }

        public async Task Delete(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.OffersDetail.Include(c=>c.Offers).Include(c => c.Products).Where(c => c.Id == Id).FirstOrDefaultAsync();
            _context.OffersDetail.Remove(model);
            await Commit();
        }

        public async Task<bool> Exists(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            return await _context.OffersDetail.AnyAsync(a => a.Id == Id);
        }

        public async Task<OffersDetail> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.OffersDetail.Include(c => c.Offers).Include(c => c.Products).Where(e => e.Id == Id).FirstOrDefaultAsync();
            var model = _mapper.Map<OffersDetail>(data);
            return model;
        }

        public async Task<List<OffersDetail>> GetAll()
        {
            var data = await _context.OffersDetail.Include(c => c.Offers).Include(c => c.Products).ToListAsync();
            var model = _mapper.Map<List<OffersDetailEntity>, List<OffersDetail>>(data);
            return model;
        }

        public async Task<int> Update(OffersDetail offersDetail)
        {
            if (offersDetail.Id == null)
            {
                throw new ArgumentNullException(nameof(offersDetail));
            }
            var data = await _context.OffersDetail.Where(c => c.Id == offersDetail.Id).FirstOrDefaultAsync();
            var model = _mapper.Map(offersDetail, data);
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
