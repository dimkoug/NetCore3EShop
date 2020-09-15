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
    public class MediaService : IMediaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MediaService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Add(Media document)
        {
            var model = _mapper.Map<MediaEntity>(document);
            _context.Media.Add(model);
            await Commit();
            return model.Id;
        }

        public async Task Delete(int? Id)
        {
            if(Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.Media.Where(c => c.Id == Id).FirstOrDefaultAsync();
            var productsPhotos = await _context.ProductMedia.Where(c => c.MediaId == Id).ToListAsync();
            if(productsPhotos.Count <= 1)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\uploaded_files", model.MediaPath);
                System.IO.File.Delete(filePath);
                _context.Media.Remove(model);
            }
            
            await Commit();
        }

        public async Task<Media> Get(int? Id)
        {
            if(Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }


            var model = await _context.Media.Where(c => c.Id == Id).Include(c => c.ProductMedia).FirstOrDefaultAsync();
            var data = _mapper.Map<Media>(model);
            return data;
        }

        public async Task<List<Media>> GetAll()
        {
            var model = await _context.Media.Include(c => c.ProductMedia).ToListAsync();
            var data = _mapper.Map<List<MediaEntity>, List<Media>>(model);
            return data;
        }

        public async Task<int> Update(Media document)
        {
            var model = await _context.Media.Where(c => c.Id == document.Id).Include(c => c.ProductMedia).FirstOrDefaultAsync();
            var data = _mapper.Map(document, model);
            _context.Update(data);
            await Commit();
            return data.Id;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddFile(Media model, IFormFile File)
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


                model.MediaPath = File.FileName;
            }
        }

        public async Task<bool> Exists(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            return await _context.Media.AnyAsync(a => a.Id == Id);
        }

        public async Task AddFiles(ICollection<IFormFile> Files)
        {
            if (Files.Count > 0)
            {
                foreach(var ufile in Files)
                {
                    if (ufile != null && ufile.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\uploaded_files", ufile.FileName);

                        // If file with same name exists delete it
                        if (System.IO.File.Exists(ufile.FileName))
                        {
                            System.IO.File.Delete(ufile.FileName);
                        }

                        // Create new local file and copy contents of uploaded file
                        //using (var localFile = System.IO.File.OpenWrite(filePath))
                        using (var uploadedFile = new FileStream(filePath, FileMode.Create))
                        {
                            await ufile.CopyToAsync(uploadedFile);
                        }

                        var model = new Media{ 
                        MediaPath = ufile.FileName,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now

                        };
                        await Add(model);
                    }
                }
            }
        }
    }
}
