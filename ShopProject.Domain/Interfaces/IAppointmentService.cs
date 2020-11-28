using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAll();

        Task<Appointment> Get(int? Id);

        Task<Appointment> Add(Appointment viewModel);
        Task<Appointment> Update(Appointment viewModel);
        Task Remove(int? Id);
        Task<bool> Exists(int? Id);
    }
}
