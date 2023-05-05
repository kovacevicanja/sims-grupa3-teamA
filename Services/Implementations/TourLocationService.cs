using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class TourLocationService : ITourLocationService
    {
        public ITourLocationRepository _tourLocationRepository { get; set; }
        public TourLocationService() { }
        public void Initialize()
        {
            _tourLocationRepository = Injector.CreateInstance<ITourLocationRepository>();
        }
        public void Create(Location location)
        {
            _tourLocationRepository.Create(location);   
        }
        public List<Location> GetAll()
        {
            return _tourLocationRepository.GetAll();
        }
        public Location GetById(int id)
        {
            return _tourLocationRepository.GetById(id);
        }
        public void Save()
        {
            _tourLocationRepository.Save();
        }
    }
}