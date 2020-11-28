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
    public class PressReleaseService: IPressReleaseService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public PressReleaseService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PressRelease> Add(PressRelease viewModel)
        {
            var model = _mapper.Map<PressReleaseEntity>(viewModel);
            _context.PressReleases.Add(model);
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
            return await _context.PressReleases.AnyAsync(c => c.Id == Id);
        }

        public async Task<PressRelease> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.PressReleases.FirstOrDefaultAsync(c => c.Id == Id);
            var viewModel = _mapper.Map<PressRelease>(data);
            return viewModel;
        }

        public async Task AddFile(PressRelease viewModel, IFormFile file)
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


                viewModel.MediaPath = file.FileName;
            }
        }

        public async Task<List<PressRelease>> GetAll()
        {
            var data = await _context.PressReleases.ToListAsync();
            var viewList = _mapper.Map<List<PressReleaseEntity>, List<PressRelease>>(data);
            return viewList;
        }

        public async Task Remove(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.PressReleases.FirstOrDefaultAsync(c => c.Id == Id);
            _context.PressReleases.Remove(data);
            await _context.SaveChangesAsync();
        }

        public async Task<PressRelease> Update(PressRelease viewModel)
        {
            if (viewModel.Id == null)
            {
                throw new ArgumentNullException(nameof(viewModel.Id));
            }
            var data = await _context.PressReleases.FirstOrDefaultAsync(c => c.Id == viewModel.Id);
            _mapper.Map(viewModel, data);
            _context.Update(data);
            await _context.SaveChangesAsync();
            return viewModel;
        }
    }
}
