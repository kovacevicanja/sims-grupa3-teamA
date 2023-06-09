using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Domain.Enums;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using BookingProject.View.CustomMessageBoxes;
using System;
using System.Collections.Generic;
using System.IO;
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
                if (tourRequest.ComplexTourRequestId == complexTourRequestId)
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
        private bool AcceptanceDeadline(ComplexTourRequest complexTourRequest)
        {
            var sortedList = complexTourRequest.TourRequestsList.OrderBy(t => t.Id).ToList();
            int flag = 0;

            if (sortedList.Count > 0)
            {
                if (DateTime.Now >= sortedList[0].EndDate.AddHours(-48)
                    && sortedList[0].Status != TourRequestStatus.ACCEPTED)
                {
                    foreach (TourRequest tourRequest in sortedList)
                    {
                        if (tourRequest.Status == TourRequestStatus.ACCEPTED && tourRequest != sortedList[0])
                        {
                            flag = 1;
                        }
                    }
                    if (flag == 0)
                    {
                        _complexTourRequestRepository.ChnageStatusToInvalid(complexTourRequest);
                        return true;
                    }
                }
            }

            return false;
        }

        public void ChnageStatusComplexTourRequest(int guestId)
        {
            int flag = 0;
            int count = 0;
            foreach (ComplexTourRequest complexTourRequest in GetGuestComplexRequests(guestId))
            {
                if (!AcceptanceDeadline(complexTourRequest))
                {
                    count = complexTourRequest.TourRequestsList.Count;
                    while (count != 0 && flag != 1)
                    {
                        foreach (TourRequest tourRequest in complexTourRequest.TourRequestsList)
                        {
                            if (tourRequest.Status != TourRequestStatus.ACCEPTED)
                            {
                                flag = 1;
                                break;
                            }
                            count--;
                        }

                    }
                    if (flag == 0)
                    {
                        _complexTourRequestRepository.ChnageStatus(complexTourRequest);
                    }
                    flag = 0;
                }
            }
        }
    }
}