using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingProject.Commands;
using System.ComponentModel;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using BookingProject.Localization;

namespace BookingProject.View.GuideViewModel
{
    public class ChangeStatsYearViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        public RelayCommand CancelCommand { get; }
        public RelayCommand SetYearCommand { get; }
        public ChangeStatsYearViewModel()
        {
            PickedYear = "2023";
            CancelCommand = new RelayCommand(Cancel_Button_Click, CanExecute);
            SetYearCommand = new RelayCommand(Button_Click_Set, CanExecute);

        }
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
        private string _pickedYear;
        public string PickedYear
        {
            get => _pickedYear;
            set
            {
                if (value != _pickedYear)
                {
                    _pickedYear = value;
                    IsButtonEnabled = NumberValidation();
                    OnPropertyChanged();
                }
            }
        }
        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(ChangeStatsYearWindow)) { window.Close(); }
            }
        }
        private void Cancel_Button_Click(object param)
        {
            TourStatisticsWindow tourStatisticsWindow = new TourStatisticsWindow("all");
            tourStatisticsWindow.Show();
            CloseWindow();
        }
        private bool NumberValidation()
        {
            if (PickedYear == null) 
            {
                return false;
            }
            Regex yearRegex = new Regex(@"^\d{4}$");
            Match yearMatch = yearRegex.Match(PickedYear);
            if (yearMatch.Success)
            {
                return true;
            }
            return false;
        }
        private void Button_Click_Set(object param)
        {
            if (!IsButtonEnabled) {
                return;
            }
            TourStatisticsWindow tourStatisticsWindow = new TourStatisticsWindow(PickedYear);
            tourStatisticsWindow.Show();
            CloseWindow();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Error => null;
        public string this[string propertyName]
        {
            get
            {
                if (propertyName == "PickedYear")
                {
                    if ((TranslationSource.Instance.CurrentCulture.Name).Equals("en-US"))
                    {
                        if (string.IsNullOrEmpty(PickedYear) && !NumberValidation())
                            return "ENTER A YEAR IN \"YYYY\" FORMAT!";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(PickedYear) && !NumberValidation())
                            return "UNESI GODINU U \"YYYY\" FORMATU!";

                    }
                }
                return null;
            }
        }
    }
}
