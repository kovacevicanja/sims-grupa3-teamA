using BookingProject.DependencyInjection;
using BookingProject.Domain;
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
        TourRequest GetByID(int id);
        void Save(List<TourRequest> tourRequests);
        void SaveTourRequest();
    }
}