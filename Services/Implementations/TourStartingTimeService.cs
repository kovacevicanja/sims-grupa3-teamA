using BookingProject.Model;
using BookingProject.Repositories.Implementations;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class TourStartingTimeService : ITourStartingTimeService
    {
        public List<TourDateTime> _dates;
        public TourStartingTimeRepository tourStartingTimeRepository;
        public TourStartingTimeService()
        {
            _dates = new List<TourDateTime>();
            tourStartingTimeRepository = new TourStartingTimeRepository();
            Load();
        }
        public void Load()
        {
            _dates = tourStartingTimeRepository.GetAll();
        }
        public void LinkToTour(int id)
        {
            foreach (TourDateTime startingDate in _dates)
            {
                if (startingDate.TourId == -1)
                {
                    startingDate.TourId = id;
                }
            }
        }
        public void CleanUnused()
        {
            _dates.RemoveAll(d => d.TourId == -1);
        }
    }
}