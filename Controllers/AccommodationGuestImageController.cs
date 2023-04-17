using BookingProject.DependencyInjection;
using BookingProject.Domain.Images;
using BookingProject.FileHandler;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controllers
{
    public class AccommodationGuestImageController
    {
        private readonly IAccommodationGuestImageService _accommodationGuestImageService;
        public AccommodationGuestImageController()
        {
            _accommodationGuestImageService = Injector.CreateInstance<IAccommodationGuestImageService>();
        }

        public List<AccommodationGuestImage> GetAll()
        {
            return _accommodationGuestImageService.GetAll();
        }

        public void Create(AccommodationGuestImage image) { _accommodationGuestImageService.Create(image); }
        public AccommodationGuestImage GetByID(int id)
        {
            return _accommodationGuestImageService.GetByID(id);
        }
    }
}
