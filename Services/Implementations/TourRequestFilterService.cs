using BookingProject.ConversionHelp;
using BookingProject.DependencyInjection;
using BookingProject.Domain.Enums;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;

namespace BookingProject.Services.Implementations
{

    public class TourRequestFilterService : ITourRequestFilterService
    {
        private ITourRequestService _tourRequestService;
        public TourRequestFilterService() { }
        public void Initialize()
        {
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
        }
   
        public bool RequestedCity(TourRequest tour, string city)
        {
            if (city.Equals("") || tour.Location.City.ToLower().Contains(city.ToLower())) { return true; }
            else { return false; }
        }

        public bool RequestedYear(TourRequest tour, string year)
        {
            if (year.Equals("") || tour.StartDate.Year.ToString().Equals(year)) { return true; }
            else { return false; }
        }

        public bool RequestedMonth(TourRequest tour, string month)
        {
            int parsedMonth;

            if (month.Equals("")) return true;
            else if (int.TryParse(month, out parsedMonth))
            {
                if (month.Equals("") || (tour.StartDate.Month <= parsedMonth && tour.EndDate.Month >= parsedMonth)) return true;
                else return false;
            }
            else return false;
        }

        public bool RequestedCountry(TourRequest tour, string country)
        {
            if (country.Equals("") || tour.Location.Country.ToLower().Contains(country.ToLower())) { return true; }
            else { return false; }
        }
        public bool RequestedLanguage(TourRequest tour, string choosenLanguage)
        {
            string languageEnum = tour.Language.ToString().ToLower();

            if (choosenLanguage.Equals("") || languageEnum.Equals(choosenLanguage.ToLower())) { return true; }
            else { return false; }
        }
        public bool RequestedNumOfGuests(TourRequest tour, string numOfGuests)
        {
            if (numOfGuests.Equals("") || int.Parse(numOfGuests) <= tour.GuestsNumber) { return true; }
            else { return false; }
        }

        public bool RequestedStartDate(TourRequest tour, string startDate)
        {
            if (startDate.IsEmpty())
            {
                return true;
            }
            if (!(DateTime.TryParse(startDate, out DateTime result) && startDate.Length == 19))
            {
                return false;
            }

            if (DateConversion.StringToDateTour(startDate) <= tour.StartDate) { return true; }
            else { return false; }
        }

        public bool RequestedEndDate(TourRequest tour, string endDate)
        {
            if (endDate.IsEmpty())
            {
                return true;
            }
            if (!(DateTime.TryParse(endDate, out DateTime result) && endDate.Length == 19))
            {
                return false;
            }
            if (DateConversion.StringToDateTour(endDate) >= tour.EndDate) { return true; }
            else { return false; }
        }
        public List<TourRequest> PendingTours()
        {
            List<TourRequest> pendingTours = new List<TourRequest>();
            foreach (TourRequest tour in _tourRequestService.GetAll())
            {
                if (tour.Status == TourRequestStatus.PENDING)
                {
                    pendingTours.Add(tour);
                }
            }
            return pendingTours;
        }
        public bool WantedTour(TourRequest tour, string city, string country, string choosenLanguage, string numOfGuests, string startDate, string endDate)
        {
            if (RequestedCity(tour, city)
                && RequestedCountry(tour, country)
                && RequestedLanguage(tour, choosenLanguage)
                && RequestedNumOfGuests(tour, numOfGuests)
                && (RequestedStartDate(tour, startDate)
                && RequestedEndDate(tour, endDate)))
            { return true; }
            else { return false; }
        }
        public bool WantedFilteredTour(TourRequest tour, string city, string country, string choosenLanguage, string year, string month)
        {
            if (RequestedCity(tour, city)
                && RequestedCountry(tour, country)
                && RequestedLanguage(tour, choosenLanguage)
                && RequestedYear(tour, year)
                && RequestedMonth(tour, month))
            { return true; }
            else { return false; }
        }
    }
}
