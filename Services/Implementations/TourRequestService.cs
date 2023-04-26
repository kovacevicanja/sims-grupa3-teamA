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
        public int AcceptedRequestsNumber(int guestId, string enteredYear = "")
        {
            int acceptedRequestsNumber = 0;

            foreach (TourRequest tourRequest in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                if (tourRequest.Status == TourRequestStatus.ACCEPTED)
                {
                    if (enteredYear == "")
                    {
                        acceptedRequestsNumber++;
                    }
                    else if (tourRequest.StartDate.Year.ToString().Equals(enteredYear) && tourRequest.EndDate.Year.ToString().Equals(enteredYear))
                    { 
                        acceptedRequestsNumber++;
                    }
                }
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
        private int UnacceptedRequestsNumber (int guestId, string enteredYear = "")
        {
            int unacceptedRequestsNumber = 0;
            
            foreach (TourRequest tourRequest in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                if (tourRequest.Status == TourRequestStatus.INVALID)
                {
                    if (enteredYear == "")
                    {
                        unacceptedRequestsNumber++;
                    }
                    else if (tourRequest.StartDate.Year.ToString().Equals(enteredYear) && tourRequest.EndDate.Year.ToString().Equals(enteredYear))
                    {
                        unacceptedRequestsNumber++;
                    }
                }
            }

            return unacceptedRequestsNumber;
        }
        public int GetNumberRequestsLanguage(int guestId, LanguageEnum language, string enteredYear = "")
        {
            int numberRequestsLanguage = 0;

            foreach (TourRequest tourRequest in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                if (tourRequest.Language == language)
                {
                    if (enteredYear == "")
                    {
                        numberRequestsLanguage++;
                    }
                    else if (tourRequest.StartDate.Year.ToString() == enteredYear && tourRequest.EndDate.Year.ToString() == enteredYear)
                    {
                        numberRequestsLanguage++;
                    }
                }
            }
            
            return numberRequestsLanguage;
        }
        public int GetNumberRequestsLocation(int guestId, string country, string city, string enteredYear = "")
        {
            int numberRequestsLocation = 0;

            foreach (TourRequest tourRequest in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                if (tourRequest.Location.City == city && tourRequest.Location.Country == country)
                {
                    if (enteredYear == "")
                    {
                        numberRequestsLocation++;
                    }
                    else if (tourRequest.StartDate.Year.ToString() == enteredYear && tourRequest.EndDate.Year.ToString() == enteredYear)
                    {
                        numberRequestsLocation++;
                    }
                }
            }

            return numberRequestsLocation;
        }
        public double GetAvarageNumberOfPeopleInAcceptedRequests(int guestId, string enteredYear = "")
        {
            int totalNumberOfPeople = FindTotalNumber(guestId, enteredYear)[0];
            int totalNumberOfAcceptedRequests = FindTotalNumber(guestId, enteredYear)[1];

            if (totalNumberOfAcceptedRequests == 0)
            {
                return 0;
            }

            return (double)(totalNumberOfPeople / totalNumberOfAcceptedRequests);
        }
        private List<int> FindTotalNumber(int guestId, string enteredYear = "")
        {
            List<int> totalNumberList = new List<int>();
            int people = 0;
            int acceptedRequests = 0;

            foreach (TourRequest tourRequest in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                if (tourRequest.Status == TourRequestStatus.ACCEPTED)
                {
                    if (enteredYear == "")
                    {
                        people += tourRequest.GuestsNumber;
                        acceptedRequests++;
                    }
                    else if (tourRequest.StartDate.Year.ToString().Equals(enteredYear) && tourRequest.EndDate.Year.ToString().Equals(enteredYear))
                    {
                        people += tourRequest.GuestsNumber;
                        acceptedRequests++;
                    }
                }
            }

            totalNumberList.Add(people);
            totalNumberList.Add(acceptedRequests);

            return totalNumberList;
        }
    }
}