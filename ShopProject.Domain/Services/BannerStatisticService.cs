using AutoMapper;
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
    public class BannerStatisticService: IBannerStatisticService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public BannerStatisticService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Add(Banner viewModel)
        {
            var model = _mapper.Map<BannerEntity>(viewModel);
            //_context.BannerStatistics.Add(model);
            await _context.SaveChangesAsync();
            _mapper.Map(model, viewModel);
            
        }
 
    }
}
