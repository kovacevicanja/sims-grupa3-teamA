using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        void SendNotification(User guest, Tour CreatedTour);
        List<TourRequest> GetGuestRequests(int guestId, string enteredYear);
        double GetUnacceptedRequestsPercentage(int guestId, string enteredYear);
        double GetAcceptedRequestsPercentage(int guestId, string enteredYear);
        double GetAvarageNumberOfPeopleInAcceptedRequests(int guestId, string enteredYear);
        bool RequestedCity(TourRequest tour, string city);
        bool RequestedCountry(TourRequest tour, string country);
        bool RequestedLanguage(TourRequest tour, string choosenLanguage);
        bool RequestedNumOfGuests(TourRequest tour, string numOfGuests);
        bool RequestedStartDate(TourRequest tour, string startDate);
        bool RequestedEndDate(TourRequest tour, string endDate);
        bool RequestedYear(TourRequest tour, string year);
        bool RequestedMonth(TourRequest tour, string month);
        bool IsMatchingYear(int guestId, string enteredYear = "");
        void SystemSendingNotification(int guestId);
        List<TourRequest> FindUnacceptedRequestsForGuests(int guestId);
        void NewlyAcceptedRequests(int guestId);
    }
}