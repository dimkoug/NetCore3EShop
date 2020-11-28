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
    public class BlogPostMediaService : IBlogPostMediaService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public BlogPostMediaService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Add(BlogPost viewModel, ICollection<IFormFile> files)
        {

            string subPath = @"wwwroot\uploaded_files";


            if (files.Count > 0 && files !=null)
            {
                
                foreach(var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        DirectoryInfo di = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), subPath));
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), subPath, file.FileName);

                        using (var uploadedFile = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(uploadedFile);
                        }

                        var media = new MediaEntity { MediaPath = file.FileName, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
                        _context.Media.Add(media);
                        await _context.SaveChangesAsync();
                        int id = media.Id;
                        viewModel.BlogPostMedia.Add(new BlogPostMedia { MediaId = id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now });
                    }
                }
      
            }
        }

        public async Task Delete(int Id)
        {
            var items = await _context.BlogPostsMedia.Where(c => c.BlogPostId == Id).ToListAsync();
            _context.BlogPostsMedia.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
