using BookingProject.Commands;
using BookingProject.Controllers;
using BookingProject.Domain.Enums;
using BookingProject.Domain;
using BookingProject.Model.Enums;
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
using Microsoft.SqlServer.Server;

namespace BookingProject.View.GuideViewModel
{
    public class RequestStatisticsViewModel : INotifyPropertyChanged, IDataErrorInfo
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
        public RequestStatisticsViewModel()
        {
            _tourRequestController = new TourRequestController();
            TourRequests = new ObservableCollection<TourRequest>((_tourRequestController.GetAll()));
            Stats = TourRequests.Count();
            CancelCommand = new RelayCommand(Button_Cancel, CanExecute);
            SearchCommand = new RelayCommand(Button_Click_Search, CanExecute);
            ShowAllCommand = new RelayCommand(Button_Click_Show, CanExecute);
            var languages = Enum.GetValues(typeof(LanguageEnum)).Cast<LanguageEnum>();
            Languages = new ObservableCollection<LanguageEnum>(languages);

        }


        private int _stats;
        public int Stats
        {
            get => _stats;
            set
            {
                if (value != _stats)
                {
                    _stats = value;
                    OnPropertyChanged();
                }
            }
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
                        return "Format \"YYYY\" ";

                }
                if (columnName == "EndDate")
                {
                    if (!(DateTime.TryParse(EndDate, out DateTime result)) || (EndDate.Length != 19))
                        return "Enter a number in this range: 1-12  ";

                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "StartingDate" };
        private void Button_Click_Search(object param)
        {


            _tourRequestController.Filter(TourRequests, City, Country, ChosenLanguage, StartDate, EndDate);
            Stats = TourRequests.Count();

        }
        private void Button_Click_Show(object param)
        {


            _tourRequestController.ShowAll(TourRequests);
            Stats = TourRequests.Count();

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
                if (window.GetType() == typeof(RequestStatisticsView)) { window.Close(); }
            }
        }

    }
}
