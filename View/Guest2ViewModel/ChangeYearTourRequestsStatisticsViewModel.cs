using BookingProject.Commands;
using BookingProject.View.Guest2View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.WebPages;
using System.Windows;
using System.Windows.Navigation;

namespace BookingProject.View.Guest2ViewModel
{
    public class ChangeYearTourRequestsStatisticsViewModel : INotifyPropertyChanged
    {
        public int GuestId { get; set; }
        public RelayCommand ChangeYearCommand { get; }
        public RelayCommand CancelCommand { get; }
        public NavigationService NavigationService { get; set; }    
        public string PreviouesPage { get; set; }

        public ChangeYearTourRequestsStatisticsViewModel(int guestId, NavigationService navigationService, string previouesPage="")
        {
            GuestId = guestId;

            ChangeYearCommand = new RelayCommand(Button_Click_ChangeYear, CanWhenEntered); 
            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);

            NavigationService = navigationService;
            PreviouesPage = previouesPage;
        }

        private bool CanWhenEntered(object param)
        {
            string trimmedYear = EnteredYear?.Trim();
            if (string.IsNullOrEmpty(trimmedYear))
            {
                return false;
            }

            string pattern = @"^(?!0)\d{4}$";
            return Regex.IsMatch(trimmedYear, pattern);
        }

        private bool CanExecute(object param) { return true; }

        public void Button_Click_ChangeYear(object param)
        {
            if (PreviouesPage.Equals("languageChart"))
            {
                NavigationService.Navigate(new TourRequestsLanguageChartView(GuestId, NavigationService, EnteredYear));
            }
            else if (PreviouesPage.Equals("locationChart"))
            {
                NavigationService.Navigate(new TourRequestsLocationChartView(GuestId, NavigationService, EnteredYear));
            }
            else if (PreviouesPage.Equals("pieChart"))
            {
                NavigationService.Navigate(new TourRequestStatisticsPieChart(GuestId, NavigationService, EnteredYear));
            }
            else
            {
                NavigationService.Navigate(new TourRequestStatisticsView(GuestId, NavigationService, EnteredYear));

            }
        }

        public void Button_Click_Cancel(object param)
        {
            NavigationService.GoBack();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private string _enteredYear;
        public string EnteredYear
        {
            get => _enteredYear;
            set
            {
                if (value != _enteredYear)
                {
                    _enteredYear = value;
                    OnPropertyChanged(nameof(EnteredYear));
                    OnPropertyChanged(nameof(CanWhenEntered));
                }
            }
        }
    }
}