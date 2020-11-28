using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShopProject.Data;
using ShopProject.Data.Entities;
using ShopProject.Domain.Interfaces;
using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Services
{
    public class ProductService: IProductService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Product> Add(Product viewModel)
        {
            var model = _mapper.Map<ProductEntity>(viewModel);
            _context.Products.Add(model);
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
            return await _context.Products.AnyAsync(c => c.Id == Id);
        }

        public async Task<Product> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Products.Include(c => c.ProductMediaList).ThenInclude(m=>m.Media).Include(c=>c.Parent).Include(c=>c.Brand).Include(c=>c.ProductShopCategories).Include(c=>c.ProductTags).Include(c=>c.ProductAttributes).ThenInclude(p => p.FeatureAttributes).ThenInclude(f=>f.Feature).FirstOrDefaultAsync(c => c.Id == Id);
            var viewModel = _mapper.Map<Product>(data);
            return viewModel;
        }

        public async Task AddFile(Product viewModel, IFormFile file)
        {

            string subPath = @"wwwroot\uploaded_files"; // Your code goes here

            if (file != null && file.Length > 0)
            {

                DirectoryInfo di = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), subPath));
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), subPath, file.FileName);
                // If file with same name exists delete it
                //if (System.IO.File.Exists(file.FileName))
                //{
                //    System.IO.File.Delete(file.FileName);
                //}

                // Create new local file and copy contents of uploaded file
                //using (var localFile = System.IO.File.OpenWrite(filePath))
                using (var uploadedFile = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(uploadedFile);
                }


                viewModel.Hero = file.FileName;
            }
        }

        public async Task<List<Product>> GetAll()
        {
            var data = await _context.Products.Include(c=>c.ProductMediaList).ThenInclude(m => m.Media).Include(c => c.Parent).Include(c => c.Brand).Include(c => c.ProductShopCategories).Include(c => c.ProductTags).Include(c => c.ProductAttributes).ThenInclude(p=>p.FeatureAttributes).ThenInclude(f => f.Feature).ToListAsync();
            var viewList = _mapper.Map<List<ProductEntity>, List<Product>>(data);
            return viewList;
        }

        public async Task Remove(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Products.FirstOrDefaultAsync(c => c.Id == Id);
            _context.Products.Remove(data);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> Update(Product viewModel)
        {
            if (viewModel.Id == null)
            {
                throw new ArgumentNullException(nameof(viewModel.Id));
            }
            var data = await _context.Products.FirstOrDefaultAsync(c => c.Id == viewModel.Id);
            _mapper.Map(viewModel, data);
            _context.Update(data);
            await _context.SaveChangesAsync();
            return viewModel;
        }
    }
}
