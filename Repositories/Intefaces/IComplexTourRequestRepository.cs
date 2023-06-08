using BookingProject.Domain;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IComplexTourRequestRepository
    {
        void Create(ComplexTourRequest complexTourRequest);
        List<ComplexTourRequest> GetAll();
        ComplexTourRequest GetById(int id);
        void Initialize();
        void Save();
        int GenerateId();
        List<ComplexTourRequest> GetGuestComplexRequests(int guestId);
        void ChnageStatus(ComplexTourRequest complexTourRequest);
        void ChnageStatusToInvalid(ComplexTourRequest complexTourRequest);
    }
}