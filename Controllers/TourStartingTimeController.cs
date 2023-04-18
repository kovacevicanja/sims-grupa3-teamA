using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Serializer;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class TourStartingTimeController
    {
        private readonly ITourStartingTimeService _tourStartingTimeService;
        public TourStartingTimeController()
        {
            _tourStartingTimeService = Injector.CreateInstance<ITourStartingTimeService>();
        }
        public void LinkToTour(int id)
        {
            _tourStartingTimeService.LinkToTour(id);
        }
        public void CleanUnused()
        {
            _tourStartingTimeService.CleanUnused();
        }
        public void Create(TourDateTime date)
        {
            _tourStartingTimeService.Create(date);
        }
        public List<TourDateTime> GetAll()
        {
            return _tourStartingTimeService.GetAll();   
        }
        public TourDateTime GetByID(int id)
        {
            return _tourStartingTimeService.GetByID(id);
        }
        public void Save()
        {
            _tourStartingTimeService.Save();
        }
    }
}