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
    public class OrdersDetailService : IOrdersDetailService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrdersDetailService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Add(OrdersDetail orderDetail)
        {
            var model = _mapper.Map<OrdersDetailEntity>(orderDetail);
            _context.OrdersDetail.Add(model);
            await Commit();
            return model.Id;
        }

        public async Task Delete(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.OrdersDetail.Where(c => c.Id == Id).FirstOrDefaultAsync();
            _context.OrdersDetail.Remove(model);
            await Commit();
        }

        public async Task<OrdersDetail> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.OrdersDetail.Include(c => c.Orders).Where(c => c.Id == Id).FirstOrDefaultAsync();
            var data = _mapper.Map<OrdersDetail>(model);
            return data;
        }

        public async Task<List<OrdersDetail>> GetAll()
        {
            var model = await _context.OrdersDetail.Include(c => c.Orders).ToListAsync();
            var data = _mapper.Map<List<OrdersDetailEntity>, List<OrdersDetail>>(model);
            return data;
        }

        public async Task<int> Update(OrdersDetail orderDetail)
        {
            var model = await _context.Features.Where(c => c.Id == orderDetail.Id).FirstOrDefaultAsync();
            var data = _mapper.Map(orderDetail, model);
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
