using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Services;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.Controller
{
    public class TourController
    {
        private readonly ITourService _tourService;
        public TourController()
        {
            _tourService = Injector.CreateInstance<ITourService>();
        }
        public void Create(Tour tour)
        {
            _tourService.Create(tour);
        }
        public List<Tour> GetAll()
        {
            return _tourService.GetAll();
        }
        public Tour GetById(int id)
        {
            return _tourService.GetById(id);
        }
        public bool WantedTour(Tour tour, string city, string country, string duration, string chosenLanguage, string numOfGuests)
        {
            return _tourService.WantedTour(tour, city, country, duration, chosenLanguage, numOfGuests);
        }
        public bool RequestedCity(Tour tour, string city)
        {
            return _tourService.RequestedCity(tour, city);
        }
        public bool RequestedCountry(Tour tour, string country)
        {
            return _tourService.RequestedCountry(tour, country);
        }
        public bool RequestedDuration(Tour tour, string duration)
        {
            return _tourService.RequestedDuration(tour, duration);
        }
        public bool RequestedLanguage(Tour tour, string chosenLanguage)
        {
            return _tourService.RequestedLanguage(tour, chosenLanguage);
        }
        public bool RequestedNumOfGuests(Tour tour, string numOfGuests)
        {
            return _tourService.RequestedNumOfGuests(tour, numOfGuests);
        }
        public ObservableCollection<Tour> Search(ObservableCollection<Tour> tourView, string city, string country, string duration, string chosenLanguage, string numOfGuests)
        {
            return _tourService.Search(tourView, city, country, duration, chosenLanguage, numOfGuests);
        }
        public void ShowAll(ObservableCollection<Tour> tourView)
        {
            _tourService.ShowAll(tourView);
        }
        public Tour GetLastTour()
        {
            return _tourService.GetLastTour();
        }
        public List<Tour> GetFilteredTours(Location location, DateTime selectedDate)
        {
            return _tourService.GetFilteredTours(location, selectedDate);
        }
        public void GoThroughTourDates(Tour tour, DateTime selectedDate)
        {
            _tourService.GoThroughTourDates(tour, selectedDate);
        }
        public void GoThroughBookedToursDates(Tour tour, DateTime selectedDate, TourDateTime tdt)
        {
            _tourService.GoThroughBookedToursDates(tour, selectedDate, tdt);
        }

        public void BindLastTour()
        {
            _tourService.BindLastTour();
        }

    }
}