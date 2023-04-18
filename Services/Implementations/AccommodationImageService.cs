using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Model.Images;
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
    public class AccommodationImageService : IAccommodationImageService
    {
        private IAccommodationImageRepository _imageRepository;
        public AccommodationImageService() { }
        public void Initialize()
        {
            _imageRepository= Injector.CreateInstance<IAccommodationImageRepository>();
        }
        public void Create(AccommodationImage image)
        {
            _imageRepository.Create(image);
        }
        public void Save(List<AccommodationImage> images)
        {
            _imageRepository.Save(images);
        }
        public void SaveImage()
        {
            _imageRepository.SaveImage();
        }
        public List<AccommodationImage> GetAll()
        {
            return _imageRepository.GetAll();
        }

        public AccommodationImage GetByID(int id)
        {
            return _imageRepository.GetByID(id);
        }

        public void LinkToAccommodation(int id)
        {

            foreach (AccommodationImage image in _imageRepository.GetAll())
            {
                if (image.AccommodationId == -1)
                {
                    image.AccommodationId = id;
                }

            }
        }
        public void DeleteUnused()
        {
            _imageRepository.GetAll().RemoveAll(i => i.AccommodationId == -1);

        }
    }
}
