using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShopProject3.DataAccess;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Services
{
    public class ProductMediaService : IProductMediaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductMediaService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Add(Products product, string[] items)
        {
            throw new NotImplementedException();
        }

        public async Task AddFiles(Products product, ICollection<IFormFile> Files)
        {
            if (Files.Count > 0)
            {
                foreach(var file in Files)
                {
                    var model = await _context.Media.Where(f => f.MediaPath == file.FileName).FirstOrDefaultAsync();
                    product.ProductMedia.Add(new ProductMedia { MediaId = model.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now });
                }
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
