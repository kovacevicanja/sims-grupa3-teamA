using BookingProject.DependencyInjection;
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
        public TourImageService()
        {
            _tourImageRepository = Injector.CreateInstance<ITourImageRepository>();
        }
        public void CleanUnused()
        {
            _tourImageRepository.GetAll().RemoveAll(i => i.Tour.Id == -1);
        }
    }
}