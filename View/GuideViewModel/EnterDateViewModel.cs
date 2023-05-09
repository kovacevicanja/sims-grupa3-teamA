using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.ConversionHelp;
using BookingProject.Model;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.GuideViewModel
{
    internal class EnterDateViewModel:IDataErrorInfo,INotifyPropertyChanged
    {


        public TourStartingTimeController StartingDateController { get; set; }

        public DateConversion DateConversion { get; set; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand CreateCommand { get; }

        public EnterDateViewModel()
        {

            StartingDateController = new TourStartingTimeController();
            CancelCommand = new RelayCommand(CancelButton_Click, CanExecute);
            CreateCommand = new RelayCommand(Button_Click_Kreiraj, CanExecute);
        }

        private string _startingDate;

        public string StartingDate
        {
            get => _startingDate;
            set
            {
                if (value != _startingDate)
                {
                    _startingDate = value;
                    IsButtonEnabled = ValidateTime();
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_Kreiraj(object param)
        {
            TourDateTime startingDate = new TourDateTime();
            startingDate.StartingDateTime = DateConversion.StringToDateTour(StartingDate);
            StartingDateController.Create(startingDate);
            StartingDateController.Save();

        }

        private void CancelButton_Click(object param)
        {
            CloseWindow();
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "StartingDate")
                {
                    if (!(DateTime.TryParse(StartingDate, out DateTime result)) || (StartingDate.Length != 19))
                        return "Format dd/mm/yyyy hh:mm:ss";

                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "StartingDate" };

        private bool _isButtonEnabled = false;
        public bool IsButtonEnabled
        {
            get { return _isButtonEnabled; }
            set
            {
                if (_isButtonEnabled != value)
                {
                    _isButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ValidateTime()
        {
            if (DateTime.TryParse(StartingDate, out DateTime result) && StartingDate.Length == 19)
            {
                return true;
            }

            return false;
        }
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                if (DateTime.TryParse(StartingDate, out DateTime result) && StartingDate.Length == 19)
                {
                    return true;
                }

                return false;
            }
        }
        private bool CanExecute(object param) { return true; }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(EnterDate)) { window.Close(); }
            }
        }

    }
}

