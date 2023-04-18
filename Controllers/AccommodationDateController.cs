using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;

namespace BookingProject.Controller
{
    public class AccommodationDateController 
    {
        private readonly IAccommodationDateService _accommodationDateService;
        public AccommodationDateController()
        {
            _accommodationDateService = Injector.CreateInstance<IAccommodationDateService>();
        }

        public List<AccommodationDate> GetAll()
        {
            return _accommodationDateService.GetAll();
        }
        
        public AccommodationDate GetByID(int id)
        {
            return _accommodationDateService.GetByID(id);
        }
    }
}