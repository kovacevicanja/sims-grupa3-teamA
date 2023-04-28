using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model.Enums;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
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
    }
}