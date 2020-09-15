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
    public class OffersService: IOffersService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OffersService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<int> Add(Offers offers)
        {
            var model = _mapper.Map<OffersEntity>(offers);
            _context.Offers.Add(model);
            await Commit();
            return model.Id;
        }

        public async Task Delete(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.Offers.Where(c => c.Id == Id).FirstOrDefaultAsync();
            _context.Offers.Remove(model);
            await Commit();
        }

        public async Task<bool> Exists(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            return await _context.Offers.AnyAsync(a => a.Id == Id);
        }

        public async Task<Offers> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Offers.Include(c => c.OffersDetail).Where(e => e.Id == Id).FirstOrDefaultAsync();
            var model = _mapper.Map<Offers>(data);
            return model;
        }

        public async Task<List<Offers>> GetAll()
        {
            var data = await _context.Offers.Include(c => c.OffersDetail).ToListAsync();
            var model = _mapper.Map<List<OffersEntity>, List<Offers>>(data);
            return model;
        }

        public async Task<int> Update(Offers offers)
        {
            if (offers.Id == null)
            {
                throw new ArgumentNullException(nameof(offers));
            }
            var data = await _context.Offers.Where(c => c.Id == offers.Id).FirstOrDefaultAsync();
            var model = _mapper.Map(offers, data);
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
