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
        List<TourRequest> GetGuestRequests(int guestId, string enteredYear);
        double GetUnacceptedRequestsPercentage(int guestId, string enteredYear);
        double GetAcceptedRequestsPercentage(int guestId, string enteredYear);
        int GetNumberRequestsLanguage(int guestId, LanguageEnum language, string enteredYear);
        int GetNumberRequestsLocation(int guestId, string country, string city, string enteredYear);
        double GetAvarageNumberOfPeopleInAcceptedRequests(int guestId, string enteredYear);
        bool WantedTour(TourRequest tour, string city, string country, string choosenLanguage, string numOfGuests, string startDate, string endDate);
        bool RequestedCity(TourRequest tour, string city);
        bool RequestedCountry(TourRequest tour, string country);
        bool RequestedLanguage(TourRequest tour, string choosenLanguage);
        bool RequestedNumOfGuests(TourRequest tour, string numOfGuests);
        bool RequestedStartDate(TourRequest tour, string startDate);
        bool RequestedEndDate(TourRequest tour, string endDate);
        ObservableCollection<TourRequest> Search(ObservableCollection<TourRequest> tourView, string city, string country, string choosenLanguage, string numOfGuests, string startDate, string endDate);
        void ShowAll(ObservableCollection<TourRequest> tourView);

    }
}