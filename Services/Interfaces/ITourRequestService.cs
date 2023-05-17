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
        List<TourRequest> FindUnacceptedRequestsForGuests(int guestId);
        void NewlyAcceptedRequests(int guestId);
        List<int> GuestsForNotification();
    }
}