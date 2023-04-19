using BookingProject.Controller;
using BookingProject.Model.Images;
using BookingProject.Model;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using BookingProject.Repositories;
using BookingProject.Services.Interfaces;
using BookingProject.DependencyInjection;
using BookingProject.Repositories.Intefaces;
using BookingProject.Repositories.Implementations;
using BookingProject.View.CustomMessageBoxes;

namespace BookingProject.Services
{
    public class TourService : ITourService
    {
        private ITourRepository _tourRepository;
        public TourService() { }
        public void Initialize()
        {
            _tourRepository = Injector.CreateInstance<ITourRepository>();
        }
        public bool WantedTour(Tour tour, string city, string country, string duration, string choosenLanguage, string numOfGuests)
        {
            if (RequestedCity(tour, city)
                && RequestedCountry(tour, country)
                && RequestedDuration(tour, duration)
                && RequestedLanguage(tour, choosenLanguage)
                && RequestedNumOfGuests(tour, numOfGuests)) { return true; }
            else { return false; }
        }
        public bool RequestedCity(Tour tour, string city)
        {
            if (city.Equals("") || tour.Location.City.ToLower().Contains(city.ToLower())) { return true; }
            else { return false; }
        }
        public bool RequestedCountry(Tour tour, string country)
        {
            if (country.Equals("") || tour.Location.Country.ToLower().Contains(country.ToLower())) { return true; }
            else { return false; }
        }
        public bool RequestedDuration(Tour tour, string duration)
        {
            if (duration.Equals("") || double.Parse(duration) == tour.DurationInHours) { return true; }
            else { return false; }
        }
        public bool RequestedLanguage(Tour tour, string choosenLanguage)
        {
            string languageEnum = tour.Language.ToString().ToLower();

            if (choosenLanguage.Equals("") || languageEnum.Equals(choosenLanguage.ToLower())) { return true; }
            else { return false; }
        }
        public bool RequestedNumOfGuests(Tour tour, string numOfGuests)
        {
            if (numOfGuests.Equals("") || int.Parse(numOfGuests) <= tour.MaxGuests) { return true; }
            else { return false; }
        }
        public ObservableCollection<Tour> Search(ObservableCollection<Tour> tourView, string city, string country, string duration, string choosenLanguage, string numOfGuests)
        {
            tourView.Clear();

            foreach (Tour tour in _tourRepository.GetAll())
            {
                if (WantedTour(tour, city, country, duration, choosenLanguage, numOfGuests))
                {
                    tourView.Add(tour);
                }
            }
            return tourView;
        }
        public void ShowAll(ObservableCollection<Tour> tourView)
        {
            tourView.Clear();

            foreach (Tour tour in _tourRepository.GetAll())
            {
                tourView.Add(tour);
            }
        }

        public void Create(Tour tour)
        {
            _tourRepository.Create(tour);
        }

        public List<Tour> GetAll()
        {
            return _tourRepository.GetAll();
        }

        public Tour GetByID(int id)
        {
            return _tourRepository.GetByID(id);
        }
        public Tour GetLastTour()
        {
            return _tourRepository.GetAll().Last();
        }

        public void BindLastTour()
        {
            _tourRepository.BindLastTour();
        }


    }
}