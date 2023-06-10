using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IComplexTourRequestService
    {
        void Initialize();
        void Create(ComplexTourRequest complexTourRequest);
        List<ComplexTourRequest> GetAll();
        ComplexTourRequest GetById(int id);
        void Save();
        void DeleteRequestIfComplexRequestNotCreated(int complexTourRequestId);
        List<ComplexTourRequest> GetGuestComplexRequests(int guestId);
        void ChnageStatusComplexTourRequest(int guestId);
    }
}