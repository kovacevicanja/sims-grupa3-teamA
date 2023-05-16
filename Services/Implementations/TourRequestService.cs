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
        private INotificationService _notificationService;
        private IUserService _userService;
        private ITourLocationService _locationService;
        private ITourService _tourService;
        public TourRequestService() { }
        public void Initialize()
        {
            _tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
            _notificationService = Injector.CreateInstance<INotificationService>();
            _userService = Injector.CreateInstance<IUserService>();
            _locationService = Injector.CreateInstance<ITourLocationService>();
            _tourService = Injector.CreateInstance<ITourService>();
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

        public void SendNotification(User guest, Tour createdTour)
        {
            Notification notification = new Notification();
            notification.UserId = guest.Id;
            notification.Text = "A tour you requested was created, go search it out, first instance of this tour will be held on  " + createdTour.StartingTime[0].StartingDateTime + "!";
            notification.Read = false;
            notification.RelatedTo = "Creating a tour on demand";
            _notificationService.Create(notification);
            //ovde bi bilo najbolje organiciti ovu notifikaciju na zahteve koji su pending, i onda samo takvim gostima poslati notifikaciju 
            //poslati gostu ciji je zahtev prihvacen notifikaciju, ako je njegov zahtev bio pending
        }

        public void SystemSendingNotification(int guestId)
        {
            foreach (int id in GuestsForNotification())
            {
                if (guestId == id)
                {
                    Notification notification = new Notification();
                    notification.UserId = id;
                    notification.Text = "The tour you always wanted was made. View the offer in newly created tours.";
                    notification.Read = false;
                    notification.RelatedTo = "System notification about new tours";
                    _notificationService.Create(notification);
                }
            }
        }

        public void NewlyAcceptedRequests(int guestId)
        {
            foreach (Tour tour in FindToursCreatedByStatistcis())
            {
                foreach (TourRequest request in FindUnacceptedRequests())
                {
                    if (tour.Language == request.Language ||
                        (tour.Location.City.Equals(request.Location.City) && tour.Location.Country.Equals(request.Location.Country)))
                    {
                        _tourRequestRepository.ChangeStatus(request, guestId);
                    }
                }
            }
        }

        private List<int> GuestsForNotification()
        {
            List<int> guests = new List<int>();
            foreach (Tour tour in FindToursCreatedByStatistcis())
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

            return guests.Distinct().ToList();
        }

        private List<Tour> FindToursCreatedByStatistcis()
        {
            return _tourService.FindToursCreatedByStatistcis();
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
                if (tourRequest.Status == TourRequestStatus.INVALID)
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
        public double GetAcceptedRequestsPercentage(int guestId, string enteredYear = "")
        {
            int requestsTotalNumber = _tourRequestRepository.GetGuestRequests(guestId, enteredYear).Count;
            int acceptedRequestsNumber = AcceptedRequestsNumber(guestId, enteredYear);

            if (requestsTotalNumber == 0) { return 0; }

            return ((double)acceptedRequestsNumber / requestsTotalNumber) * 100;
        }
        public bool IsMatchingYear(int guestId, string enteredYear = "")
        {
            foreach (var request in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                return string.IsNullOrEmpty(enteredYear) ||
                         (request.StartDate.Year.ToString().Equals(enteredYear) && request.EndDate.Year.ToString().Equals(enteredYear));
            }
            return false;
        }
        public int AcceptedRequestsNumber(int guestId, string enteredYear = "")
        {
            int acceptedRequestsNumber = 0;

            foreach (TourRequest request in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                if (request.Status == TourRequestStatus.ACCEPTED && IsMatchingYear(guestId, enteredYear)) { acceptedRequestsNumber++; }
            }

            return acceptedRequestsNumber;
        }
        public double GetUnacceptedRequestsPercentage(int guestId, string enteredYear = "")
        {
            int requestsTotalNumber = _tourRequestRepository.GetGuestRequests(guestId, enteredYear).Count;
            int unacceptedRequestsNumber = UnacceptedRequestsNumber(guestId, enteredYear);

            if (requestsTotalNumber == 0) { return 0; }

            return ((double)unacceptedRequestsNumber / requestsTotalNumber) * 100;
        }
        private int UnacceptedRequestsNumber(int guestId, string enteredYear = "")
        {
            int unacceptedRequestsNumber = 0;

            foreach (TourRequest request in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                if (request.Status == TourRequestStatus.INVALID && IsMatchingYear(guestId, enteredYear)) { unacceptedRequestsNumber++; }
            }

            return unacceptedRequestsNumber;
        }
        public double GetAvarageNumberOfPeopleInAcceptedRequests(int guestId, string enteredYear = "")
        {
            int totalNumberOfPeople = 0, totalNumberOfAcceptedRequests = 0;
            FindTotalNumber(guestId, out totalNumberOfPeople, out totalNumberOfAcceptedRequests, enteredYear);

            if (totalNumberOfAcceptedRequests == 0) { return 0; }

            return (double)(totalNumberOfPeople / totalNumberOfAcceptedRequests);
        }
        private void FindTotalNumber(int guestId, out int totalGuests, out int acceptedRequests, string enteredYear = "")
        {
            totalGuests = 0;
            acceptedRequests = 0;

            foreach (TourRequest request in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                if (request.Status == TourRequestStatus.ACCEPTED && IsMatchingYear(guestId, enteredYear))
                {
                    totalGuests += request.GuestsNumber;
                    acceptedRequests++;
                }
            }
        }
    }
}