using AutoMapper;
using ShopProject3.DataAccess;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Services
{
    public class ShoppingCartItemsService : IShoppingCartItemsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ShoppingCartItemsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Add(ShoppingCartItems shoppingCartItem)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int? Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ShoppingCartItems> Get(int? Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ShoppingCartItems>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Update(ShoppingCartItems shoppingCartItem)
        {
            throw new NotImplementedException();
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
