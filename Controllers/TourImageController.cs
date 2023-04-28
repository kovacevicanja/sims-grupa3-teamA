using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class TourImageController
    {
        private ITourImageService _tourImageService;
        public TourImageController()
        {
            _tourImageService = Injector.CreateInstance<ITourImageService>();
        }
        public void CleanUnused()
        {
            _tourImageService.CleanUnused();
        }
        public void Create(TourImage image)
        {
            _tourImageService.Create(image);
        }
        public void LinkToTour(int id)
        {
            _tourImageService.LinkToTour(id);
        }
        public List<TourImage> GetAll()
        {
            return _tourImageService.GetAll();
        }
        public TourImage GetById(int id)
        {
            return _tourImageService.GetById(id);   
        }
        public void Save()
        {
            _tourImageService.Save();
        }
    }
}