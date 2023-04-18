using BookingProject.Controller;
using BookingProject.Model;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface ITourTimeInstanceService
    {
        void Create(TourTimeInstance instance);
        List<TourTimeInstance> GetAll();
        TourTimeInstance GetByID(int id);
        void Initialize();
    }
}
