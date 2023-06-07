using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using BookingProject.View.CustomMessageBoxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class ComplexTourRequestService : IComplexTourRequestService
    {
        private IComplexTourRequestRepository _complexTourRequestRepository;
        private ITourRequestService _tourRequestService;

        public ComplexTourRequestService()
        {

        }
        public void Initialize()
        {
            _complexTourRequestRepository = Injector.CreateInstance<IComplexTourRequestRepository>();
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
        }
        public void Create(ComplexTourRequest complexTourRequest)
        {
            _complexTourRequestRepository.Create(complexTourRequest);
        }
        public List<ComplexTourRequest> GetAll()
        {
            return _complexTourRequestRepository.GetAll();
        }

        public ComplexTourRequest GetById(int id)
        {
            return _complexTourRequestRepository.GetById(id);
        }

        public void Save()
        {
            _complexTourRequestRepository.Save();
        }

        public void DeleteRequestIfComplexRequestNotCreated(int complexTourRequestId)
        {
            List<TourRequest> tourRequests = _tourRequestService.GetAll();
            List<TourRequest> tourRequestsCopy = new List<TourRequest>(tourRequests);

            foreach (TourRequest tourRequest in tourRequestsCopy)
            {
                if (tourRequest.ComplexTourRequest.Id == complexTourRequestId)
                {
                    tourRequests.Remove(tourRequest);
                }
            }
            _tourRequestService.Save(tourRequests);
        }

        public List<ComplexTourRequest> GetGuestComplexRequests(int guestId)
        {
            return _complexTourRequestRepository.GetGuestComplexRequests(guestId);
        }
    }
}