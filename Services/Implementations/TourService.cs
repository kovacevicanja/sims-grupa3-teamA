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
using BookingProject.Domain;

namespace BookingProject.Services
{
    public class TourService : ITourService
    {
        private ITourRepository _tourRepository;
        private ITourReservationRepository _tourReservationRepository;
        private ITourRequestService _tourRequestService;
        public CustomMessageBox CustomMessageBox { get; set; }

        public TourService()
        {
            CustomMessageBox = new CustomMessageBox();
        }
        public void Initialize()
        {
            _tourRepository = Injector.CreateInstance<ITourRepository>();
            _tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
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

        public void FullBind()
        {
            _tourRepository.TourLocationBind();
            _tourRepository.BindTourImage();
            _tourRepository.TourKeyPointBind();
            _tourRepository.TourDateBind();
        }
        public Tour GetById(int id)
        {
            return _tourRepository.GetById(id);
        }
        public Tour GetLastTour()
        {
            return _tourRepository.GetAll().Last();
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
                foreach (TourRequest request in _tourRequestService.FindUnacceptedRequestsForGuests(guestId))
                {
                    if (request.Language == tour.Language)
                    {
                        tours.Add(tour);
                    }
                }
            }

            return tours;
        }

        public List<Tour> FilterToursByDate(DateTime selectedDate) 
        {
            List<Tour> _tours = Injector.CreateInstance<ITourService>().GetAll();
            List<Tour> filteredTours = new List<Tour>();

            foreach (Tour tour in _tours)
            {
                GoThroughTourDates(tour, selectedDate);
            }

            return filteredTours;
        }
        public void GoThroughTourDates(Tour tour, DateTime selectedDate)
        {
            List<TourDateTime> startingTimeCopy = tour.StartingTime.ToList();

            foreach (TourDateTime tdt in startingTimeCopy)
            {
                if (tdt.StartingDateTime == selectedDate)
                {
                    tour.StartingTime.Remove(tdt);
                }
                else
                {
                    GoThroughBookedToursDates(tour, selectedDate, tdt);
                }
            }
        }
        public void GoThroughBookedToursDates(Tour tour, DateTime selectedDate, TourDateTime tdt)
        {
            foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if (tourReservation.GuestsNumberPerReservation == 0 && tourReservation.ReservationStartingTime == tdt.StartingDateTime)
                {
                    tour.StartingTime.Remove(tdt);
                }
            }
        }
        public List<Tour> FilterToursByLocation(List<Tour> filteredTours, Location location, DateTime selectedDate)
        {
            List<Tour> filteredToursCopy = new List<Tour>(filteredTours);
            List<Tour> _tours = Injector.CreateInstance<ITourService>().GetAll();

            foreach (Tour tour in _tours)
            {
                if (tour.Location.City == location.City && tour.Location.Country == location.Country && tour.StartingTime.Count != 0)
                {
                    filteredTours.Add(tour);
                }
            }
            return filteredTours;
        }
        public List<Tour> GetFilteredTours(Location location, DateTime selectedDate)
        {
            List<Tour> filteredTours = FilterToursByDate(selectedDate);
            filteredTours = FilterToursByLocation(filteredTours, location, selectedDate);

            if (filteredTours.Count == 0) { CustomMessageBox.ShowCustomMessageBox("Unfortunately, it is not possible to make a reservation. All tours at that location are booked."); }

            return filteredTours;
        }

        public void BindLastTour()
        {
            _tourRepository.BindLastTour();
        }

        public List<Tour> LoadAgain()
        {
            return _tourRepository.LoadAgain();
        }
    }
}