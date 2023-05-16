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
            _userService= Injector.CreateInstance<IUserService>();
            _locationService= Injector.CreateInstance<ITourLocationService>();
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
            notification.Text = "A tour you requested was created, go search it out, first instance of this tour will be held on  "+createdTour.StartingTime[0].StartingDateTime+"!";
            notification.Read = false;
            notification.RelatedTo = "Creating a tour on demand";
            _notificationService.Create(notification);
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

        public List<TourRequest> GetGuestRequests (int guestId, string enteredYear = "")
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
        private bool IsMatchingYear (int guestId, string enteredYear = "")
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
        public int GetNumberRequestsLanguage(int guestId, LanguageEnum language, string enteredYear = "")
        {
            int numberRequestsLanguage = 0;

            foreach (TourRequest request in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                if (request.Language == language && IsMatchingYear(guestId, enteredYear)) { numberRequestsLanguage++; }
            }
            
            return numberRequestsLanguage;
        }

        public int GetNumberAllRequestsLanguage(LanguageEnum language, string enteredYear = "")
        {
            int numberAllRequestsLanguage = 0;
            foreach (User user in _userService.GetAll())
            {
                if (user.UserType == UserType.GUEST2)
                {
                    numberAllRequestsLanguage += GetNumberRequestsLanguage(user.Id, language, enteredYear);
                }
            }
            return numberAllRequestsLanguage;
        }

        public LanguageEnum GetTopLanguage(string enteredYear = "")
        {
            LanguageEnum TopLanguage = LanguageEnum.SERBIAN;
            int numberAllRequestsLanguage = GetNumberAllRequestsLanguage(LanguageEnum.SERBIAN, enteredYear);
            if (numberAllRequestsLanguage < GetNumberAllRequestsLanguage(LanguageEnum.GERMAN, enteredYear))
            {
                TopLanguage = LanguageEnum.GERMAN;
                numberAllRequestsLanguage = GetNumberAllRequestsLanguage(LanguageEnum.GERMAN, enteredYear);
            }
            if (numberAllRequestsLanguage < GetNumberAllRequestsLanguage(LanguageEnum.ENGLISH, enteredYear))
            {
                TopLanguage = LanguageEnum.ENGLISH;
            }
            if (numberAllRequestsLanguage < GetNumberAllRequestsLanguage(LanguageEnum.SPANISH, enteredYear))
            {
                TopLanguage = LanguageEnum.SPANISH;
            }

            return TopLanguage;
        }
     
        public int GetNumberRequestsLocation(int guestId, string country, string city, string enteredYear = "")
        {
            int numberRequestsLocation = 0;

            foreach (TourRequest request in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                if (request.Location.City == city && request.Location.Country == country && IsMatchingYear(guestId, enteredYear)) 
                { numberRequestsLocation++; }
            }

            return numberRequestsLocation;
        }

        public int GetNumberAllRequestsLocation(string country, string city, string enteredYear = "")
        {
            int numberAllRequestsLocation = 0;
            foreach (User user in _userService.GetAll())
            {
                if (user.UserType == UserType.GUEST2)
                {
                    numberAllRequestsLocation += GetNumberRequestsLocation(user.Id, country, city, enteredYear);
                }
            }
            return numberAllRequestsLocation;
        }

        public Location GetTopLocation(string enteredYear = "")
        {
            Location topLocation = new Location();
            topLocation.Country = "Default";
            topLocation.City = "Default";

            int numberAllRequestsLanguage = 0;
            foreach (Location location in _locationService.GetAll())
            {
                if (numberAllRequestsLanguage < GetNumberAllRequestsLocation(location.Country, location.City, enteredYear))
                {
                    numberAllRequestsLanguage = GetNumberAllRequestsLocation(location.Country, location.City, enteredYear);
                    topLocation.Country = location.Country;
                    topLocation.City = location.City;
                }
            }
            return topLocation;
        }

        public bool WantedTour(TourRequest tour, string city, string country, string choosenLanguage, string numOfGuests, string startDate, string endDate)
        {
            if (RequestedCity(tour, city)
                && RequestedCountry(tour, country)               
                && RequestedLanguage(tour, choosenLanguage)
                && RequestedNumOfGuests(tour, numOfGuests)
                && (RequestedStartDate(tour, startDate)
                && RequestedEndDate(tour, endDate)))
            { return true; }
            else { return false; }
        }

        public bool WantedFilteredTour(TourRequest tour, string city, string country, string choosenLanguage, string year, string month)
        {
            if (RequestedCity(tour, city)
                && RequestedCountry(tour, country)
                && RequestedLanguage(tour, choosenLanguage)
                && RequestedYear(tour, year)
                && RequestedMonth(tour, month))
            { return true; }
            else { return false; }
        }
        public bool RequestedCity(TourRequest tour, string city)
        {
            if (city.Equals("") || tour.Location.City.ToLower().Contains(city.ToLower())) { return true; }
            else { return false; }
        }

        public bool RequestedYear(TourRequest tour, string year)
        {
            if (year.Equals("") || tour.StartDate.Year.ToString().Equals(year)) { return true; }
            else { return false; }
        }

        public bool RequestedMonth(TourRequest tour, string month)
        {
            int parsedMonth;

            if (month.Equals("")) return true;
            else if (int.TryParse(month, out parsedMonth))
            {
                if (month.Equals("") || (tour.StartDate.Month <= parsedMonth && tour.EndDate.Month >= parsedMonth)) return true;
                else return false;
            }
            else return false;
        }

        public bool RequestedCountry(TourRequest tour, string country)
        {
            if (country.Equals("") || tour.Location.Country.ToLower().Contains(country.ToLower())) { return true; }
            else { return false; }
        }
        public bool RequestedLanguage(TourRequest tour, string choosenLanguage)
        {
            string languageEnum = tour.Language.ToString().ToLower();

            if (choosenLanguage.Equals("") || languageEnum.Equals(choosenLanguage.ToLower())) { return true; }
            else { return false; }
        }
        public bool RequestedNumOfGuests(TourRequest tour, string numOfGuests)
        {
            if (numOfGuests.Equals("") || int.Parse(numOfGuests) <= tour.GuestsNumber) { return true; }
            else { return false; }
        }

        public bool RequestedStartDate(TourRequest tour, string startDate)
        {
            if (startDate.IsEmpty())
            {
                return true;
            }
            if (!(DateTime.TryParse(startDate, out DateTime result) && startDate.Length == 19))
            {
                return false;
            }

            if (DateConversion.StringToDateTour(startDate)<=tour.StartDate) { return true; }
            else { return false; }
        }

        public bool RequestedEndDate(TourRequest tour, string endDate)
        {
            if (endDate.IsEmpty())
            {
                return true;
            }
            if (!(DateTime.TryParse(endDate, out DateTime result) && endDate.Length == 19))
            {
                return false;
            }
            if (DateConversion.StringToDateTour(endDate)>=tour.EndDate) { return true; }
            else { return false; }
        }

        public ObservableCollection<TourRequest> Search(ObservableCollection<TourRequest> tourView, string city, string country, string choosenLanguage, string numOfGuests, string endDate, string startDate)
        {
            tourView.Clear();

            foreach (TourRequest tour in _tourRequestRepository.GetAll())
            {
                if (WantedTour(tour, city, country, choosenLanguage, numOfGuests, endDate, startDate))
                {
                    tourView.Add(tour);
                }
            }
            return tourView;
        }

        public ObservableCollection<TourRequest> Filter(ObservableCollection<TourRequest> tourView, string city, string country, string choosenLanguage, string year, string month)
        {
            tourView.Clear();

            foreach (TourRequest tour in _tourRequestRepository.GetAll())
            {
                if (WantedFilteredTour(tour, city, country, choosenLanguage, year, month))
                {
                    tourView.Add(tour);
                }
            }
            return tourView;
        }
        public void ShowAll(ObservableCollection<TourRequest> tourView)
        {
            tourView.Clear();

            foreach (TourRequest tour in _tourRequestRepository.GetAll())
            {
                tourView.Add(tour);
            }
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