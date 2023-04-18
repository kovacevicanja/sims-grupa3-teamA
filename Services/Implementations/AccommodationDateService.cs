using BookingProject.DependencyInjection;
using BookingProject.Model;
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
    public class AccommodationDateService : IAccommodationDateService
    {
        private IAccommodationDateRepository _accommodationDateRepository;
        public AccommodationDateService() { }
        public void Initialize()
        {
            _accommodationDateRepository = Injector.CreateInstance<IAccommodationDateRepository>();
        }

        public List<AccommodationDate> GetAll()
        {
            return _accommodationDateRepository.GetAll();
        }

        public AccommodationDate GetByID(int id)
        {
            return _accommodationDateRepository.GetByID(id);
        }
        public void Save(List<AccommodationDate> dates)
        {
            _accommodationDateRepository.Save(dates);
        }
    }
}
