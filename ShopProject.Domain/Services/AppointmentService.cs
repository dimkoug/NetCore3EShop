using AutoMapper;
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
    public class AppointmentService : IAppointmentService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public AppointmentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Appointment> Add(Appointment viewModel)
        {
            var model = _mapper.Map<AppointmentEntity>(viewModel);
            _context.Appointments.Add(model);
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
            return await _context.Appointments.AnyAsync(c => c.Id == Id);
        }

        public async Task<Appointment> Get(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Appointments.FirstOrDefaultAsync(c => c.Id == Id);
            var viewModel = _mapper.Map<Appointment>(data);
            return viewModel;
        }

        public async Task<List<Appointment>> GetAll()
        {
            var data = await _context.Appointments.ToListAsync();
            var viewList = _mapper.Map<List<AppointmentEntity>, List<Appointment>>(data);
            return viewList;
        }

        public async Task Remove(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            var data = await _context.Appointments.FirstOrDefaultAsync(c => c.Id == Id);
            _context.Appointments.Remove(data);
            await _context.SaveChangesAsync();
            
        }

        public async Task<Appointment> Update(Appointment viewModel)
        {
            if (viewModel.Id == null)
            {
                throw new ArgumentNullException(nameof(viewModel.Id));
            }
            var data = await _context.Appointments.FirstOrDefaultAsync(c => c.Id == viewModel.Id);
            _mapper.Map(viewModel, data);
            _context.Update(data);
            await _context.SaveChangesAsync();
            return viewModel;
          
        }
    }
}
