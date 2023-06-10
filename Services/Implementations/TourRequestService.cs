using BookingProject.ConversionHelp;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Domain.Enums;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Repositories;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;
using System.Windows.Input;

namespace BookingProject.Services.Implementations
{
    public class TourRequestService : ITourRequestService
    {
        private ITourRequestRepository _tourRequestRepository;
        private ITourService _tourService;
        private ITourStatisticsService _tourStatisticsService;
        public TourRequestService() { }
        public void Initialize()
        {
            _tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
            _tourService = Injector.CreateInstance<ITourService>();
            _tourStatisticsService = Injector.CreateInstance<ITourStatisticsService>();
        }
        public void Create(TourRequest tourRequest)
        {
            _tourRequestRepository.Create(tourRequest);
        }
        public List<TourRequest> GetAll()
        {
            return _tourRequestRepository.GetAll();
        }
        public TourRequest GetById(int id)
        {
            return _tourRequestRepository.GetById(id);
        }
        public void Save(List<TourRequest> tourRequests)
        {
            _tourRequestRepository.SaveTourRequest(tourRequests);
        }
        public void SaveTourRequest()
        {
            _tourRequestRepository.Save();
        }

        public void NewlyAcceptedRequests(int guestId)
        {
            foreach (Tour tour in FindToursCreatedByStatistcis())
            {
                foreach (TourRequest request in FindUnacceptedRequests())
                {
                    if (tour.Language == request.Language && request.ComplexTourRequestId == -1 ||
                        (tour.Location.City.Equals(request.Location.City) && tour.Location.Country.Equals(request.Location.Country)))
                    {
                        _tourRequestRepository.ChangeStatus(request, guestId);
                    }
                }
            }
        }

        public List<int> GuestsForNotification()
        {
            List<int> guests = new List<int>();
            foreach (Tour tour in FindToursCreatedByStatistcis())
            {
                DateTime date = tour.CreartionDate.Date.AddDays(7);
                if (date > DateTime.Now.Date)
                {
                        foreach (TourRequest request in FindUnacceptedRequests())
                        {
                            if (tour.Language == request.Language ||
                                (tour.Location.City.Equals(request.Location.City) && tour.Location.Country.Equals(request.Location.Country)))
                            {
                                guests.Add(request.Guest.Id);
                            }
                        }
                }
            }

            return guests.Distinct().ToList();
        }

        private List<Tour> FindToursCreatedByStatistcis()
        {
            return _tourStatisticsService.FindToursCreatedByStatistcis();
        }

        public List<TourRequest> FindUnacceptedRequestsForGuests(int guestId)
        {
            List<TourRequest> unacceptedRequests = new List<TourRequest>();

            foreach (TourRequest request in FindUnacceptedRequests())
            {
                if (request.Guest.Id == guestId)
                {
                    unacceptedRequests.Add(request);
                }
            }
            return unacceptedRequests;
        }

        private List<TourRequest> FindUnacceptedRequests()
        {
            List<TourRequest> unacceptedRequests = new List<TourRequest>();

            foreach (TourRequest tourRequest in GetAll())
            {
                if (tourRequest.Status == TourRequestStatus.INVALID && tourRequest.ComplexTourRequestId == -1)
                {
                    unacceptedRequests.Add(tourRequest);
                }
            }

            return unacceptedRequests;
        }

        public List<TourRequest> GetGuestRequests(int guestId, string enteredYear = "")
        {
            return _tourRequestRepository.GetGuestRequests(guestId, enteredYear);
        }
    }
}