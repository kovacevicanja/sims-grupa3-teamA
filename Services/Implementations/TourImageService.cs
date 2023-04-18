using BookingProject.DependencyInjection;
using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class TourImageService : ITourImageService
    {
        public ITourImageRepository _tourImageRepository { get; set; }
        public TourImageService() { }

        public void Initialize()
        {
            _tourImageRepository = Injector.CreateInstance<ITourImageRepository>();
        }

        public void CleanUnused()
        {
            _tourImageRepository.GetAll().RemoveAll(i => i.Tour.Id == -1);
        }

        public void Create(TourImage image)
        {
            _tourImageRepository.Create(image);
        }

        public void LinkToTour(int id)
        {
            _tourImageRepository.LinkToTour(id);
        }

        public List<TourImage> GetAll()
        {
            return _tourImageRepository.GetAll();
        }

        public TourImage GetByID(int id)
        {
            return _tourImageRepository.GetByID(id);    
        }
    }
}