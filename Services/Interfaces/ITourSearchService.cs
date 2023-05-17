using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface ITourSearchService
    {
        void Initialize();
        bool WantedTour(Tour tour, string city, string country, string duration, string choosenLanguage, string numOfGuests);
        bool RequestedCity(Tour tour, string city);
        bool RequestedCountry(Tour tour, string country);
        bool RequestedDuration(Tour tour, string duration);
        bool RequestedLanguage(Tour tour, string choosenLanguage);
        bool RequestedNumOfGuests(Tour tour, string numOfGuests);
        ObservableCollection<Tour> Search(ObservableCollection<Tour> tourView, string city, string country, string duration, string choosenLanguage, string numOfGuests);
        void ShowAll(ObservableCollection<Tour> tourView);
        List<Tour> FilterToursByDate(DateTime selectedDate);
        void GoThroughTourDates(Tour tour, DateTime selectedDate);
        void GoThroughBookedToursDates(Tour tour, DateTime selectedDate, TourDateTime tdt);
        List<Tour> FilterToursByLocation(List<Tour> filteredTours, Location location, DateTime selectedDate);
        List<Tour> GetFilteredTours(Location location, DateTime selectedDate);
    }
}
