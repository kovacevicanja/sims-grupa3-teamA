using BookingProject.Domain.Enums;
using BookingProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.DependencyInjection;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;

namespace BookingProject.Services.Implementations
{
    public class TourRequestStatisticsService : ITourRequestStatisticsService
    {
        private ITourRequestRepository _tourRequestRepository;
        public TourRequestStatisticsService() { }
        public void Initialize()
        {
            _tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
        }
        public double GetAcceptedRequestsPercentage(int guestId, string enteredYear = "")
        {
            int requestsTotalNumber = _tourRequestRepository.GetGuestRequests(guestId, enteredYear).Count;
            int acceptedRequestsNumber = AcceptedRequestsNumber(guestId, enteredYear);

            if (requestsTotalNumber == 0) { return 0; }

            return ((double)acceptedRequestsNumber / requestsTotalNumber) * 100;
        }
        public bool IsMatchingYear(int guestId, string enteredYear = "")
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
