using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Repositories;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class AccommodationLocationService : IAccommodationLocationService
    {
        private IAccommodationLocationRepository _locationRepository;
        public AccommodationLocationService() { }

        public void Initialize()
        {
            _locationRepository = Injector.CreateInstance<IAccommodationLocationRepository>();
        }
        public void Create(Location location)
        {
            _locationRepository.Create(location);
        }

        public List<Location> GetAll()
        {
            return _locationRepository.GetAll();
        }
        public void SaveLocation()
        {
            _locationRepository.SaveLocation();
        }

        public Location GetById(int id)
        {
            return _locationRepository.GetById(id);
        }
        public void Save(List<Location> locations)
        {
            _locationRepository.Save(locations);
        }
        public void Delete(Location acc)
        {
            _locationRepository.Delete(acc);
        }
    }
}
