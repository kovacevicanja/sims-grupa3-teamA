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
    public class AccommodationLocationController 
    {
        private readonly IAccommodationLocationService _locationService;
        public AccommodationLocationController()
        {
            _locationService = Injector.CreateInstance<IAccommodationLocationService>();
        }
        public void Create(Location location)
        {
            _locationService.Create(location);
        }
        public List<Location> GetAll()
        {
            return _locationService.GetAll();
        }
        public Location GetByID(int id)
        {
            return _locationService.GetByID(id);
        }
    }
}
