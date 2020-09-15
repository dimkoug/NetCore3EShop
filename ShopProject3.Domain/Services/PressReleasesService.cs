using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopProject3.DataAccess;
using ShopProject3.DataAccess.Entities;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject3.Domain.Services
{
    public class PressReleasesService: IPressReleasesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PressReleasesService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<int> Add(PressReleases pressRelease)
        {
            var model = _mapper.Map<PressReleasesEntity>(pressRelease);
            _context.PressReleases.Add(model);
            await Commit();
            return model.Id;
        }

        public async Task Delete(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var model = await _context.PressReleases.Where(c => c.Id == Id).FirstOrDefaultAsync();
            _context.PressReleases.Remove(model);
            await Commit();
        }

        public async Task<bool> Exists(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            return await _context.PressReleases.AnyAsync(a => a.Id == Id);
        }

        public async Task<PressReleases> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.PressReleases.Where(e => e.Id == Id).FirstOrDefaultAsync();
            var model = _mapper.Map<PressReleases>(data);
            return model;
        }

        public async Task<List<PressReleases>> GetAll()
        {
            var data = await _context.PressReleases.ToListAsync();
            var model = _mapper.Map<List<PressReleasesEntity>, List<PressReleases>>(data);
            return model;
        }

        public async Task<int> Update(PressReleases pressRelease)
        {
            if (pressRelease.Id == null)
            {
                throw new ArgumentNullException(nameof(pressRelease));
            }
            var data = await _context.PressReleases.Where(c => c.Id == pressRelease.Id).FirstOrDefaultAsync();
            var model = _mapper.Map(pressRelease, data);
            _context.Update(model);
            await Commit();
            return model.Id;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
