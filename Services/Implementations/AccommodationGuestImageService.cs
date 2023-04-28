using BookingProject.DependencyInjection;
using BookingProject.Domain.Images;
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
    public class AccommodationGuestImageService : IAccommodationGuestImageService
    {
        private IAccommodationGuestImageRepository _accommodationGuestImageRepository;
        public AccommodationGuestImageService() { }
        public void Initialize()
        {
            _accommodationGuestImageRepository = Injector.CreateInstance<IAccommodationGuestImageRepository>();
        }

        public List<AccommodationGuestImage> GetAll()
        {
            return _accommodationGuestImageRepository.GetAll();
        }

        public void Create(AccommodationGuestImage image)
        {
            _accommodationGuestImageRepository.Create(image);
        }

        public AccommodationGuestImage GetById(int id)
        {
            return _accommodationGuestImageRepository.GetById(id);
        }

        public void Save(List<AccommodationGuestImage> images)
        {
            _accommodationGuestImageRepository.Save(images);
        }

        public void SaveImage()
        {
            _accommodationGuestImageRepository.SaveImage();
        }
    }
}
