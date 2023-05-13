using BookingProject.ConversionHelp;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Domain.Enums;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Repositories;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;

namespace BookingProject.Services.Implementations
{
    public class TourRequestService : ITourRequestService
    {
        private ITourRequestRepository _tourRequestRepository;
        public TourRequestService() { }
        public void Initialize()
        {
            _tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
        }
        public void Create(TourRequest tourRequest)
        {
            _tourRequestRepository.Create(tourRequest);
        }
        public List<TourRequest> GetAll()
        {
            return _tourRequestRepository.GetAll();
        }
        public TourRequest GetById(int id)
        {
            return _tourRequestRepository.GetById(id);
        }
        public void Save(List<TourRequest> tourRequests)
        {
            _tourRequestRepository.SaveTourRequest(tourRequests);
        }
        public void SaveTourRequest()
        {
            _tourRequestRepository.Save();
        }
        public List<TourRequest> GetGuestRequests (int guestId, string enteredYear = "")
        {
            return _tourRequestRepository.GetGuestRequests(guestId, enteredYear);
        }
        public double GetAcceptedRequestsPercentage(int guestId, string enteredYear = "")
        {
            int requestsTotalNumber = _tourRequestRepository.GetGuestRequests(guestId, enteredYear).Count;
            int acceptedRequestsNumber = AcceptedRequestsNumber(guestId, enteredYear);

            if (requestsTotalNumber == 0) { return 0; }

            return ((double)acceptedRequestsNumber / requestsTotalNumber) * 100;
        }
        private bool IsMatchingYear (int guestId, string enteredYear = "")
        {
            foreach (var request in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                return string.IsNullOrEmpty(enteredYear) ||
                         (request.StartDate.Year.ToString().Equals(enteredYear) && request.EndDate.Year.ToString().Equals(enteredYear));
            }
            return false;
        }
        public int AcceptedRequestsNumber(int guestId, string enteredYear = "")
        {
            int acceptedRequestsNumber = 0;

            foreach (TourRequest request in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                if (request.Status == TourRequestStatus.ACCEPTED && IsMatchingYear(guestId, enteredYear)) { acceptedRequestsNumber++; }
            }

            return acceptedRequestsNumber;
        }
        public double GetUnacceptedRequestsPercentage(int guestId, string enteredYear = "")
        {
            int requestsTotalNumber = _tourRequestRepository.GetGuestRequests(guestId, enteredYear).Count;
            int unacceptedRequestsNumber = UnacceptedRequestsNumber(guestId, enteredYear);

            if (requestsTotalNumber == 0) { return 0; }

            return ((double)unacceptedRequestsNumber / requestsTotalNumber) * 100;
        }
        private int UnacceptedRequestsNumber(int guestId, string enteredYear = "")
        {
            int unacceptedRequestsNumber = 0;

            foreach (TourRequest request in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                if (request.Status == TourRequestStatus.INVALID && IsMatchingYear(guestId, enteredYear)) { unacceptedRequestsNumber++; }
            }

            return unacceptedRequestsNumber;
        }
        public int GetNumberRequestsLanguage(int guestId, LanguageEnum language, string enteredYear = "")
        {
            int numberRequestsLanguage = 0;

            foreach (TourRequest request in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                if (request.Language == language && IsMatchingYear(guestId, enteredYear)) { numberRequestsLanguage++; }
            }
            
            return numberRequestsLanguage;
        }
        public int GetNumberRequestsLocation(int guestId, string country, string city, string enteredYear = "")
        {
            int numberRequestsLocation = 0;

            foreach (TourRequest request in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                if (request.Location.City == city && request.Location.Country == country && IsMatchingYear(guestId, enteredYear)) 
                { numberRequestsLocation++; }
            }

            return numberRequestsLocation;
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
        public bool RequestedCity(TourRequest tour, string city)
        {
            if (city.Equals("") || tour.Location.City.ToLower().Contains(city.ToLower())) { return true; }
            else { return false; }
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

            if (DateConversion.StringToDateTour(startDate)<=tour.StartDate) { return true; }
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
            if (DateConversion.StringToDateTour(endDate)>=tour.EndDate) { return true; }
            else { return false; }
        }

        public ObservableCollection<TourRequest> Search(ObservableCollection<TourRequest> tourView, string city, string country, string choosenLanguage, string numOfGuests, string endDate, string startDate)
        {
            tourView.Clear();

            foreach (TourRequest tour in _tourRequestRepository.GetAll())
            {
                if (WantedTour(tour, city, country, choosenLanguage, numOfGuests, endDate, startDate))
                {
                    tourView.Add(tour);
                }
            }
            return tourView;
        }
        public void ShowAll(ObservableCollection<TourRequest> tourView)
        {
            tourView.Clear();

            foreach (TourRequest tour in _tourRequestRepository.GetAll())
            {
                tourView.Add(tour);
            }
        }

        public double GetAvarageNumberOfPeopleInAcceptedRequests(int guestId, string enteredYear = "")
        { 
            int totalNumberOfPeople = 0, totalNumberOfAcceptedRequests = 0;
            FindTotalNumber(guestId, out totalNumberOfPeople, out totalNumberOfAcceptedRequests, enteredYear);

            if (totalNumberOfAcceptedRequests == 0) { return 0; }

            return (double)(totalNumberOfPeople / totalNumberOfAcceptedRequests);
        }
        private void FindTotalNumber(int guestId, out int totalGuests, out int acceptedRequests, string enteredYear = "")
        {
            totalGuests = 0;
            acceptedRequests = 0;

            foreach (TourRequest request in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                if (request.Status == TourRequestStatus.ACCEPTED && IsMatchingYear(guestId, enteredYear))
                {
                    totalGuests += request.GuestsNumber;
                    acceptedRequests++;
                }
            }
        }
    }
}