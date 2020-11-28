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
    public class BlogCategoryService: IBlogCategoryService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public BlogCategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BlogCategory> Add(BlogCategory viewModel)
        {
            var model = _mapper.Map<BlogCategoryEntity>(viewModel);
            _context.BlogCategories.Add(model);
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
            return await _context.BlogCategories.AnyAsync(c => c.Id == Id);
        }

        public async Task AddFile(BlogCategory viewModel, IFormFile file)
        {

            string subPath = @"wwwroot\uploaded_files"; // Your code goes here

            if (file != null && file.Length > 0)
            {

                DirectoryInfo di = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), subPath));
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), subPath, file.FileName);
                //// If file with same name exists delete it
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

        public async Task<BlogCategory> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.BlogCategories.Include(c=>c.Parent).FirstOrDefaultAsync(c => c.Id == Id);
            var viewModel = _mapper.Map<BlogCategory>(data);
            return viewModel;
        }

        public async Task<List<BlogCategory>> GetAll()
        {
            var data = await _context.BlogCategories.Include(c => c.Parent).ToListAsync();
            var viewList = _mapper.Map<List<BlogCategoryEntity>, List<BlogCategory>>(data);
            return viewList;
        }

        public async Task Remove(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.BlogCategories.FirstOrDefaultAsync(c => c.Id == Id);
            _context.BlogCategories.Remove(data);
            await _context.SaveChangesAsync();

        }

        public async Task<BlogCategory> Update(BlogCategory viewModel)
        {
            if (viewModel.Id == null)
            {
                throw new ArgumentNullException(nameof(viewModel.Id));
            }
            var data = await _context.BlogCategories.FirstOrDefaultAsync(c => c.Id == viewModel.Id);
            _mapper.Map(viewModel, data);
            _context.Update(data);
            await _context.SaveChangesAsync();
            return viewModel;

        }
    }
}
