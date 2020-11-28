﻿using AutoMapper;
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
    public class OrderService: IOrderService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public OrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Order> Add(Order viewModel)
        {
            var model = _mapper.Map<OrderEntity>(viewModel);
            _context.Orders.Add(model);
            await _context.SaveChangesAsync();
            _mapper.Map(model, viewModel);
            return viewModel;
        }


        public async Task<int> Add(Order order, List<ShoppingCartItem> shoppingCartItems)
        {

            order.OrderDetails = new List<OrderDetail>();
            var model = _mapper.Map<OrderEntity>(order);

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetailEntity
                {
                    Quantity = shoppingCartItem.Quantity,
                    ProductId = shoppingCartItem.ProductId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now

                };
                model.OrderDetails.Add(orderDetail);
            }


            _context.Orders.Add(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<bool> Exists(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            return await _context.Orders.AnyAsync(c => c.Id == Id);
        }

        public async Task<Order> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Orders.FirstOrDefaultAsync(c => c.Id == Id);
            var viewModel = _mapper.Map<Order>(data);
            return viewModel;
        }

        public async Task<List<Order>> GetAll()
        {
            var data = await _context.Orders.ToListAsync();
            var viewList = _mapper.Map<List<OrderEntity>, List<Order>>(data);
            return viewList;
        }

        public async Task Remove(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Orders.FirstOrDefaultAsync(c => c.Id == Id);
            _context.Orders.Remove(data);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> Update(Order viewModel)
        {
            if (viewModel.Id == null)
            {
                throw new ArgumentNullException(nameof(viewModel.Id));
            }
            var data = await _context.Orders.FirstOrDefaultAsync(c => c.Id == viewModel.Id);
            _mapper.Map(viewModel, data);
            _context.Update(data);
            await _context.SaveChangesAsync();
            return viewModel;
        }
    }
}
