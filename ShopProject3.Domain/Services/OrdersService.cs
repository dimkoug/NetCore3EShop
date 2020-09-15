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
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrdersService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Add(Orders order, List<ShoppingCartItems> shoppingCartItems)
        {

            order.OrderDetails = new List<OrdersDetail>();
            var model = _mapper.Map<OrdersEntity>(order);

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrdersDetailEntity
                {
                    Quantity = shoppingCartItem.Quantity,
                    ProductId = shoppingCartItem.ProductId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                    
                };
                model.OrdersDetail.Add(orderDetail);
            }

            
            _context.Orders.Add(model);
            await Commit();
            return model.Id;
        }

        public async Task Delete(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.Orders.Where(c => c.Id == Id).FirstOrDefaultAsync();
            _context.Orders.Remove(model);
            await Commit();
        }

        public async Task<Orders> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.Orders.Include(c => c.OrdersDetail).Where(c => c.Id == Id).FirstOrDefaultAsync();
            var data = _mapper.Map<Orders>(model);
            return data;
        }

        public async Task<List<Orders>> GetAll()
        {
            var model = await _context.Orders.Include(c => c.OrdersDetail).ToListAsync();
            var data = _mapper.Map<List<OrdersEntity>, List<Orders>>(model);
            return data;
        }

        public async Task<int> Update(Orders order)
        {
            var model = await _context.Orders.Where(c => c.Id == order.Id).FirstOrDefaultAsync();
            var data = _mapper.Map(order, model);
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
            return await _context.Orders.AnyAsync(a => a.Id == Id);
        }
    }
}
