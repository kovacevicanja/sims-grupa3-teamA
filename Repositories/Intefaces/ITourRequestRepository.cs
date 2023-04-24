using BookingProject.Domain;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface ITourRequestRepository
    {
        void Save();
        void SaveTourRequest(List<TourRequest> tourRequests);
        void Initialize();
        void Create(TourRequest tourRequest);
        List<TourRequest> GetAll();
        TourRequest GetByID(int id);
        List<TourRequest> GetGuestRequests(int guestId);
    }
}