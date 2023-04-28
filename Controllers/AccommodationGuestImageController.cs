using BookingProject.DependencyInjection;
using BookingProject.Domain.Images;
using BookingProject.Model;
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
        public AccommodationGuestImage GetById(int id)
        {
            return _accommodationGuestImageService.GetById(id);
        }
        public void Save(List<AccommodationGuestImage> images)
        {
            _accommodationGuestImageService.Save(images);
        }
        public void SaveImage()
        {
            _accommodationGuestImageService.SaveImage();
        }
    }
}