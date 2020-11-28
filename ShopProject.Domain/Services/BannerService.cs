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
    public class BannerService : IBannerService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public BannerService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Banner> Add(Banner viewModel)
        {
            var model = _mapper.Map<BannerEntity>(viewModel);
            _context.Banners.Add(model);
            await _context.SaveChangesAsync();
            _mapper.Map(model, viewModel);
            return viewModel;
        }

        public async Task AddFile(Banner viewModel, IFormFile file)
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


                viewModel.ImagePath = file.FileName;
            }
        }

        public async Task<bool> Exists(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            return await _context.Banners.AnyAsync(c => c.Id == Id);
        }

        public async Task<Banner> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Banners.FirstOrDefaultAsync(c => c.Id == Id);
            var viewModel = _mapper.Map<Banner>(data);
            return viewModel;
        }

        public async Task<List<Banner>> GetAll()
        {
            var data = await _context.Banners.ToListAsync();
            var viewList = _mapper.Map<List<BannerEntity>, List<Banner>>(data);
            return viewList;
        }

        public async Task Remove(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Banners.FirstOrDefaultAsync(c => c.Id == Id);
            _context.Banners.Remove(data);
            await _context.SaveChangesAsync();

        }

        public async Task<Banner> Update(Banner viewModel)
        {
            if (viewModel.Id == null)
            {
                throw new ArgumentNullException(nameof(viewModel.Id));
            }
            var data = await _context.Banners.FirstOrDefaultAsync(c => c.Id == viewModel.Id);
            _mapper.Map(viewModel, data);
            _context.Update(data);
            await _context.SaveChangesAsync();
            return viewModel;

        }
    }
}
