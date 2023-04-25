using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Domain.Enums;
using BookingProject.Model.Enums;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public TourRequest GetByID(int id)
        {
            return _tourRequestRepository.GetByID(id);
        }
        public void Save(List<TourRequest> tourRequests)
        {
            _tourRequestRepository.SaveTourRequest(tourRequests);
        }
        public void SaveTourRequest()
        {
            _tourRequestRepository.Save();
        }
        public List<TourRequest> GetGuestRequests (int guestId)
        {
            return _tourRequestRepository.GetGuestRequests(guestId);
        }
        public double GetAcceptedRequestsPercentage(int guestId)
        {
            int requestsTotalNumber = _tourRequestRepository.GetGuestRequests(guestId).Count;
            int acceptedRequestsNumber = AcceptedRequestsNumber(guestId);

            return ((double)acceptedRequestsNumber / requestsTotalNumber) * 100;
        }
        public int AcceptedRequestsNumber(int guestId)
        {
            int acceptedRequestsNumber = 0;

            foreach (TourRequest tourRequest in _tourRequestRepository.GetGuestRequests(guestId))
            {
                if (tourRequest.Status == TourRequestStatus.ACCEPTED)
                {
                    acceptedRequestsNumber++;
                }
            }

            return acceptedRequestsNumber;
        }
        public double GetUnacceptedRequestsPercentage(int guestId)
        {
            int requestsTotalNumber = _tourRequestRepository.GetGuestRequests(guestId).Count;
            int unacceptedRequestsNumber = UnacceptedRequestsNumber(guestId);

            return ((double)unacceptedRequestsNumber / requestsTotalNumber) * 100;
        }
        private int UnacceptedRequestsNumber (int guestId)
        {
            int unacceptedRequestsNumber = 0;
            
            foreach (TourRequest tourRequest in _tourRequestRepository.GetGuestRequests(guestId))
            {
                if (tourRequest.Status == TourRequestStatus.INVALID)
                {
                    unacceptedRequestsNumber++;
                }
            }

            return unacceptedRequestsNumber;
        }
        public int GetNumberRequestsLanguage(int guestId, LanguageEnum language)
        {
            int numberRequestsLanguage = 0;

            foreach (TourRequest tourRequest in _tourRequestRepository.GetGuestRequests(guestId))
            {
                if (tourRequest.Language == language)
                {
                    numberRequestsLanguage++;
                }
            }
            
            return numberRequestsLanguage;
        }
        public int GetNumberRequestsLocation(int guestId, string country, string city)
        {
            int numberRequestsLocation = 0;

            foreach (TourRequest tourRequest in _tourRequestRepository.GetGuestRequests(guestId))
            {
                if (tourRequest.Location.City == city && tourRequest.Location.Country == country)
                {
                    numberRequestsLocation++;
                }
            }

            return numberRequestsLocation;
        }
        public double GetAvarageNumberOfPeopleInAcceptedRequests(int guestId)
        {
            int totalNumberOfPeople = FindTotalNumber(guestId)[0];
            int totalNumberOfAcceptedRequests = FindTotalNumber(guestId)[1];

            return (double)(totalNumberOfPeople / totalNumberOfAcceptedRequests);
        }
        private List<int> FindTotalNumber(int guestId)
        {
            List<int> totalNumberList = new List<int>();
            int people = 0;
            int acceptedRequests = 0;

            foreach (TourRequest tourRequest in _tourRequestRepository.GetGuestRequests(guestId))
            {
                if (tourRequest.Status == TourRequestStatus.ACCEPTED)
                {
                    people += tourRequest.GuestsNumber;
                    acceptedRequests++;
                }
            }

            totalNumberList.Add(people);
            totalNumberList.Add(acceptedRequests);

            return totalNumberList;
        }
    }
}