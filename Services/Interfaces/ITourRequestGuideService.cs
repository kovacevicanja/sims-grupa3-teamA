using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface ITourRequestGuideService
    {
        void Initialize();
        List<TourRequest> PendingTours();
        ObservableCollection<TourRequest> Search(ObservableCollection<TourRequest> tourView, string city, string country, string choosenLanguage, string numOfGuests, string startDate, string endDate);
        ObservableCollection<TourRequest> Filter(ObservableCollection<TourRequest> tourView, string city, string country, string choosenLanguage, string year, string month);
        void ShowAll(ObservableCollection<TourRequest> tourView);

        int GetNumberRequestsLanguage(int guestId, LanguageEnum language, string enteredYear);
        int GetNumberAllRequestsLanguage(LanguageEnum language, string enteredYear);
        int GetNumberAllRequestsLocation(string country, string city, string enteredYear);
        Location GetTopLocation(string enteredYear);
        int GetNumberRequestsLocation(int guestId, string country, string city, string enteredYear);
        LanguageEnum GetTopLanguage(string enteredYear);
    }
}
