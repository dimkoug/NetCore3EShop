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
    public class OfferDetailService : IOfferDetailService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public OfferDetailService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OfferDetail> Add(OfferDetail viewModel)
        {
            var model = _mapper.Map<OfferDetailEntity>(viewModel);
            _context.OfferDetails.Add(model);
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
            return await _context.OfferDetails.AnyAsync(c => c.Id == Id);
        }

        public async Task<OfferDetail> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.OfferDetails.FirstOrDefaultAsync(c => c.Id == Id);
            var viewModel = _mapper.Map<OfferDetail>(data);
            return viewModel;
        }

        public async Task<List<OfferDetail>> GetAll()
        {
            var data = await _context.OfferDetails.ToListAsync();
            var viewList = _mapper.Map<List<OfferDetailEntity>, List<OfferDetail>>(data);
            return viewList;
        }

        public async Task Remove(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.OfferDetails.FirstOrDefaultAsync(c => c.Id == Id);
            _context.OfferDetails.Remove(data);
            await _context.SaveChangesAsync();

        }

        public async Task<OfferDetail> Update(OfferDetail viewModel)
        {
            if (viewModel.Id == null)
            {
                throw new ArgumentNullException(nameof(viewModel.Id));
            }
            var data = await _context.OfferDetails.FirstOrDefaultAsync(c => c.Id == viewModel.Id);
            _mapper.Map(viewModel, data);
            _context.Update(data);
            await _context.SaveChangesAsync();
            return viewModel;

        }
    }
}
