using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model.Enums;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface ITourRequestService
    {
        void Initialize();
        void Create(TourRequest tourRequest);
        List<TourRequest> GetAll();
        TourRequest GetById(int id);
        void Save(List<TourRequest> tourRequests);
        void SaveTourRequest();
        List<TourRequest> GetGuestRequests(int guestId, string enteredYear);
        double GetUnacceptedRequestsPercentage(int guestId, string enteredYear);
        double GetAcceptedRequestsPercentage(int guestId, string enteredYear);
        int GetNumberRequestsLanguage(int guestId, LanguageEnum language, string enteredYear);
        int GetNumberRequestsLocation(int guestId, string country, string city, string enteredYear);
        double GetAvarageNumberOfPeopleInAcceptedRequests(int guestId, string enteredYear);
    }
}