using BookingProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface ITourRequestFilterService
    {
        void Initialize();
        bool RequestedCity(TourRequest tour, string city);
        bool RequestedCountry(TourRequest tour, string country);
        bool RequestedLanguage(TourRequest tour, string choosenLanguage);
        bool RequestedNumOfGuests(TourRequest tour, string numOfGuests);
        bool RequestedStartDate(TourRequest tour, string startDate);
        bool RequestedEndDate(TourRequest tour, string endDate);
        bool RequestedYear(TourRequest tour, string year);
        bool RequestedMonth(TourRequest tour, string month);
        List<TourRequest> PendingTours();
        bool WantedFilteredTour(TourRequest tour, string city, string country, string choosenLanguage, string year, string month);
        bool WantedTour(TourRequest tour, string city, string country, string choosenLanguage, string numOfGuests, string startDate, string endDate);
    }
}
