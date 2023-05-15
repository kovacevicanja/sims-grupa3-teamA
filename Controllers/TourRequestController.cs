using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Services;
using BookingProject.Services.Implementations;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controllers
{
    public class TourRequestController
    {
        private readonly ITourRequestService _tourRequestService;
        public TourRequestController()
        {
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
        }
        public void Create(TourRequest tourRequest)
        {
            _tourRequestService.Create(tourRequest);
        }
        public List<TourRequest> GetAll()
        {
            return _tourRequestService.GetAll();
        }
        public TourRequest GetById(int id)
        {
            return _tourRequestService.GetById(id);
        }
        public void Save(List<TourRequest> _tourRequests)
        {
            _tourRequestService.Save(_tourRequests);
        }
        public void SaveTourRequest()
        {
            _tourRequestService.SaveTourRequest();
        }
        public void SendNotification(User guest, Tour tour)
        {
            _tourRequestService.SendNotification(guest, tour);
        }

        public List<TourRequest> GetGuestRequests (int guestId, string enteredYear)
        {
            return _tourRequestService.GetGuestRequests(guestId, enteredYear);
        }
        public double GetUnacceptedRequestsPercentage(int guestId, string enteredYear)
        {
            return _tourRequestService.GetUnacceptedRequestsPercentage(guestId, enteredYear);
        }
        public double GetAcceptedRequestsPercentage(int guestId, string enteredYear)
        {
            return _tourRequestService.GetAcceptedRequestsPercentage(guestId, enteredYear);
        }
        public int GetNumberRequestsLanguage(int guestId, LanguageEnum language, string enteredYear) 
        {
            return _tourRequestService.GetNumberRequestsLanguage(guestId, language, enteredYear);
        }
        public int GetNumberRequestsLocation(int guestId, string country, string city, string enteredYear)
        {
            return _tourRequestService.GetNumberRequestsLocation(guestId, country, city, enteredYear);
        }
        public double GetAvarageNumberOfPeopleInAcceptedRequests(int guestId, string enteredYear)
        {
            return _tourRequestService.GetAvarageNumberOfPeopleInAcceptedRequests(guestId, enteredYear);
        }

        public ObservableCollection<TourRequest> Search(ObservableCollection<TourRequest> tourView, string city, string country, string chosenLanguage, string numOfGuests, string startDate, string endDate)
        {
            return _tourRequestService.Search(tourView, city, country, chosenLanguage, numOfGuests, startDate, endDate);
        }

        public ObservableCollection<TourRequest> Filter(ObservableCollection<TourRequest> tourView, string city, string country, string chosenLanguage, string year, string month)
        {
            return _tourRequestService.Filter(tourView, city, country, chosenLanguage, year, month);
        }
        public void ShowAll(ObservableCollection<TourRequest> tourRequestView)
        {
            _tourRequestService.ShowAll(tourRequestView);
        }

        public LanguageEnum GetTopLanguage(string year)
        {
            return _tourRequestService.GetTopLanguage(year);
        }

        public Location GetTopLocation(string year)
        {

            return _tourRequestService.GetTopLocation(year);
        }

    }
}