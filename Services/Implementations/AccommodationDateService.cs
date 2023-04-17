using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class AccommodationDateService
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
    }
}
