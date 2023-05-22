using BookingProject.Controller;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Repositories;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class TourStatisticsService : ITourStatisticsService
    {
        private ITourRepository _tourRepository;
        private ITourRequestService _tourRequestService;

        public TourStatisticsService() { }

        public void Initialize()
        {
            _tourRepository = Injector.CreateInstance<ITourRepository>();
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
        }

        public List<Tour> FindToursCreatedByStatistcis()
        {
            return _tourRepository.FindToursCreatedByStatistcis();
        }

        public List<Tour> FindToursCreatedByStatistcisForGuest(int guestId)
        {
            List<Tour> tours = new List<Tour>();

            foreach (Tour tour in FindToursCreatedByStatistcis())
            {
                DateTime date = tour.CreartionDate.Date.AddDays(7);
                if (date > DateTime.Now.Date)
                {
                   foreach (TourRequest request in _tourRequestService.FindUnacceptedRequestsForGuests(guestId))
                   {
                       if (request.Language == tour.Language ||
                            (tour.Location.City.Equals(request.Location.City) && tour.Location.Country.Equals(request.Location.Country)))
                       {
                            tours.Add(tour);
                       }
                   }
                }
            }
            return tours;
        }
    }
}