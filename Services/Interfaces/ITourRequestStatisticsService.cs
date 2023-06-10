using BookingProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface ITourRequestStatisticsService
    {
        void Initialize();
        double GetUnacceptedRequestsPercentage(int guestId, string enteredYear);
        double GetAcceptedRequestsPercentage(int guestId, string enteredYear);
        double GetAvarageNumberOfPeopleInAcceptedRequests(int guestId, string enteredYear);
        bool IsMatchingYear(int guestId, string enteredYear = "");
        int NumberOfAcceptedRequests(int guestId, string enteredYear = "");
        List<TourRequest> AcceptedRequestsList(int guestId, string enteredYear = "");
    }
}
