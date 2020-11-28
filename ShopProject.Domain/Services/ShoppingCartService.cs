using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class ShoppingCartService: IShoppingCartService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public ShoppingCartService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentException(nameof(httpContextAccessor));
            _session = httpContextAccessor.HttpContext.Session;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        async Task IShoppingCartService.AddToCart(string SessionCartId, int productId)
        {
            var shoppingCartItem =
                   await _context.ShoppingCartItems.SingleOrDefaultAsync(
                       s => s.ProductId == productId && s.SessionCartId == SessionCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItemEntity
                {
                    SessionCartId = SessionCartId,
                    ProductId = productId,
                    Quantity = 1,
                    CreatedAt = DateTime.Now,
                
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Quantity++;
            }
            await _context.SaveChangesAsync();
        }

        async Task IShoppingCartService.ClearCart(string SessionCartId)
        {
            var cartItems = await _context
                .ShoppingCartItems
                .Where(cart => cart.SessionCartId == SessionCartId).ToArrayAsync();

            _context.ShoppingCartItems.RemoveRange(cartItems);

            await _context.SaveChangesAsync();
        }

        async Task<List<ShoppingCartItem>> IShoppingCartService.GetShoppingCartItems(string SessionCartId)
        {
            var data = await _context.ShoppingCartItems.Include(c => c.Product).Where(c => c.SessionCartId == SessionCartId).ToListAsync();
            var items = _mapper.Map<List<ShoppingCartItemEntity>, List<ShoppingCartItem>>(data);
            return items.ToList();
        }

        async Task<decimal> IShoppingCartService.GetShoppingCartTotal(string? SessionCartId)
        {
            var items = await _context.ShoppingCartItems.Include(c => c.Product).Where(c => c.SessionCartId == SessionCartId).ToListAsync();

            decimal total = 0;
            foreach (var item in items)
            {
                total += (decimal)item.Product.Price * item.Quantity;
            }
            return total;
        }

        async Task<int> IShoppingCartService.RemoveFromCart(string SessionCartId, int productId)
        {
            var shoppingCartItem =
                    _context.ShoppingCartItems.SingleOrDefault(
                        s => s.ProductId == productId && s.SessionCartId == SessionCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Quantity > 1)
                {
                    shoppingCartItem.Quantity--;
                    localAmount = shoppingCartItem.Quantity;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            await _context.SaveChangesAsync();

            return localAmount;
        }
    }
}
