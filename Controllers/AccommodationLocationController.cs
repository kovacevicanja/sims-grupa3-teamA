using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Services.Implementations;
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
        public Location GetById(int id)
        {
            return _locationService.GetById(id);
        }
        public void Save(List<Location> locations)
        {
            _locationService.Save(locations);
        }
        public void SaveLocation()
        {
            _locationService.SaveLocation();
        }
        public void Delete(Location l)
        {
            _locationService.Delete(l);
        }
    }
}
