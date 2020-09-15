using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShopProject3.DataAccess;
using ShopProject3.DataAccess.Entities;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Services
{
    public class BannersService : IBannersService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BannersService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<int> Add(Banners banner)
        {
            var model = _mapper.Map<BannersEntity>(banner);
            _context.Banners.Add(model);
            await Commit();
            return model.Id;
        }

        public async Task Delete(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.Banners.Where(c => c.Id == Id).FirstOrDefaultAsync();
            _context.Banners.Remove(model);
            await Commit();
        }

        public async Task<bool> Exists(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            return await _context.Banners.AnyAsync(a => a.Id == Id);
        }

        public async Task<Banners> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Banners.Where(e => e.Id == Id).FirstOrDefaultAsync();
            var model = _mapper.Map<Banners>(data);
            return model;
        }

        public async Task<List<Banners>> GetAll()
        {
            var data = await _context.Banners.ToListAsync();
            var model = _mapper.Map<List<BannersEntity>, List<Banners>>(data);
            return model;
        }

        public async Task<int> Update(Banners banner)
        {
            if (banner.Id == null)
            {
                throw new ArgumentNullException(nameof(banner));
            }
            var data = await _context.Banners.Where(c => c.Id == banner.Id).FirstOrDefaultAsync();
            var model = _mapper.Map(banner, data);
            _context.Update(model);
            await Commit();
            return model.Id;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddFile(Banners model, IFormFile File)
        {
            if (File != null && File.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\uploaded_files", File.FileName);

                // If file with same name exists delete it
                if (System.IO.File.Exists(File.FileName))
                {
                    System.IO.File.Delete(File.FileName);
                }

                // Create new local file and copy contents of uploaded file
                //using (var localFile = System.IO.File.OpenWrite(filePath))
                using (var uploadedFile = new FileStream(filePath, FileMode.Create))
                {
                    await File.CopyToAsync(uploadedFile);
                }


                model.BannerImagePath = File.FileName;
            }
        }
    }
}
