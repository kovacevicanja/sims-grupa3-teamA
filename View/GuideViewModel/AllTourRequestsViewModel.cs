using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Domain.Enums;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.View.Guest2View;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.GuideViewModel
{
    public class AllTourRequestsViewModel: INotifyPropertyChanged, IDataErrorInfo
    {
        public TourRequestController _tourRequestController;
        public ObservableCollection<TourRequest> TourRequests { get; set; }

        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ChosenLanguage { get; set; } = string.Empty;
        public string NumOfGuests { get; set; } = string.Empty;

        public string EndDate { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;

        public ObservableCollection<LanguageEnum> Languages { get; set; }

        public RelayCommand CancelCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ShowAllCommand { get; }
        public RelayCommand PickCommand { get; }
        public TourRequest ChosenRequest { get; set; }
        public AllTourRequestsViewModel()
        {
            _tourRequestController = new TourRequestController();
            TourRequests = new ObservableCollection<TourRequest>(FilterRequests(_tourRequestController.GetAll()));

            CancelCommand = new RelayCommand(Button_Cancel, CanExecute);
            SearchCommand = new RelayCommand(Button_Click_Search, CanExecute);
            ShowAllCommand = new RelayCommand(Button_Click_Show, CanExecute);
            PickCommand = new RelayCommand(Button_Click_Pick, CanExecute);
            var languages = Enum.GetValues(typeof(LanguageEnum)).Cast<LanguageEnum>();
            Languages = new ObservableCollection<LanguageEnum>(languages);

        }

        public List<TourRequest> FilterRequests(List<TourRequest> requests)
        {
            List<TourRequest> filtered = new List<TourRequest>();
            foreach (var request in requests) {
                if (request.Status == TourRequestStatus.PENDING)
                {
                    filtered.Add(request);
                }
            }
            return filtered;
        }

        private bool CanExecute(object param) { return true; }

        private void Button_Cancel(object param)
        {
            GuideHomeWindow guideHomeWindow = new GuideHomeWindow();
            guideHomeWindow.Show();
            CloseWindow();
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "StartDate")
                {
                    if (!(DateTime.TryParse(StartDate, out DateTime result)) || (StartDate.Length != 19))
                        return "Format dd/mm/yyyy hh:mm:ss";

                }
                if (columnName == "EndDate")
                {
                    if (!(DateTime.TryParse(EndDate, out DateTime result)) || (EndDate.Length != 19))
                        return "Format dd/mm/yyyy hh:mm:ss";

                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "StartingDate" };
        private void Button_Click_Search(object param)
        {


                _tourRequestController.Search(TourRequests, City, Country, ChosenLanguage, NumOfGuests, StartDate, EndDate);

        }
        private void Button_Click_Show(object param)
        {

            _tourRequestController.ShowAll(TourRequests, true);

        }
        private void Button_Click_Pick(object param)
        {
            if (ChosenRequest == null)
            {
                return;
            }

            RequestedTourCreation creation = new RequestedTourCreation(ChosenRequest);
            creation.Show();
            CloseWindow();

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(AllTourRequestsView)) { window.Close(); }
            }
        }

    }
}
