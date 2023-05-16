using BookingProject.ConversionHelp;
using BookingProject.DependencyInjection;
using BookingProject.Domain.Enums;
using BookingProject.Domain;
using BookingProject.Model.Enums;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;
using System.Runtime.InteropServices.ComTypes;
namespace BookingProject.Services.Implementations
{
    public class TourRequestGuideService : ITourRequestGuideService
    {
        private ITourRequestRepository _tourRequestRepository; 
        private IUserService _userService;
        private ITourRequestService _tourRequestService;
        private ITourLocationService _locationService;
        private ITourRequestFilterService _tourRequestFilterService;
        public TourRequestGuideService() { }
        public void Initialize()
        {
            _tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
            _userService = Injector.CreateInstance<IUserService>();
            _locationService = Injector.CreateInstance<ITourLocationService>();
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
            _tourRequestFilterService=Injector.CreateInstance<ITourRequestFilterService>();
        }
        public int GetNumberRequestsLanguage(int guestId, LanguageEnum language, string enteredYear = "")
        {
            int numberRequestsLanguage = 0;
            foreach (TourRequest request in _tourRequestRepository.GetGuestRequests(guestId, enteredYear))
            {
                if (request.Language == language && _tourRequestService.IsMatchingYear(guestId, enteredYear)) { numberRequestsLanguage++; }
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
                TopLanguage = LanguageEnum.GERMAN; numberAllRequestsLanguage = GetNumberAllRequestsLanguage(LanguageEnum.GERMAN, enteredYear);
            }
            if (numberAllRequestsLanguage < GetNumberAllRequestsLanguage(LanguageEnum.ENGLISH, enteredYear))
            {
                TopLanguage = LanguageEnum.ENGLISH; numberAllRequestsLanguage = GetNumberAllRequestsLanguage(LanguageEnum.ENGLISH, enteredYear);

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
                if (request.Location.City == city && request.Location.Country == country && _tourRequestService.IsMatchingYear(guestId, enteredYear))
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
            Location topLocation = new Location(); topLocation.Country = "Default"; topLocation.City = "Default";
            int numberAllRequestsLanguage = 0;
            foreach (Location location in _locationService.GetAll())
            {
                if (numberAllRequestsLanguage < GetNumberAllRequestsLocation(location.Country, location.City, enteredYear))
                {
                    numberAllRequestsLanguage = GetNumberAllRequestsLocation(location.Country, location.City, enteredYear); topLocation.Country = location.Country; topLocation.City = location.City;
                }
            }
            return topLocation;
        }
        public ObservableCollection<TourRequest> Search(ObservableCollection<TourRequest> tourView, string city, string country, string choosenLanguage, string numOfGuests, string endDate, string startDate)
        {
            tourView.Clear();
            foreach (TourRequest tour in _tourRequestFilterService.PendingTours())
            {
                if (_tourRequestFilterService.WantedTour(tour, city, country, choosenLanguage, numOfGuests, endDate, startDate))
                {
                    tourView.Add(tour);
                }
            }
            return tourView;
        }
        public ObservableCollection<TourRequest> Filter(ObservableCollection<TourRequest> tourView, string city, string country, string choosenLanguage, string year, string month)
        {
            tourView.Clear();
            foreach (TourRequest tour in _tourRequestFilterService.PendingTours())
            {
                if (_tourRequestFilterService.WantedFilteredTour(tour, city, country, choosenLanguage, year, month))
                {
                    tourView.Add(tour);
                }
            }
            return tourView;
        }
        public void ShowAll(ObservableCollection<TourRequest> tourView, bool onlyPending)
        {
            tourView.Clear();
            if (onlyPending)
            {
                foreach (TourRequest tour in _tourRequestFilterService.PendingTours())
                {
                    tourView.Add(tour);
                }
            }
            else
            {
                foreach (TourRequest tour in _tourRequestRepository.GetAll())
                {
                    tourView.Add(tour);
                }
            }
        }
    }
}
