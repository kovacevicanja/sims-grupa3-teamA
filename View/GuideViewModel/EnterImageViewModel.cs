using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model.Images;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.GuideViewModel
{
    public class EnterImageViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        public TourImageController ImageController { get; set; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand CreateCommand { get; }

        public EnterImageViewModel()
        {

            ImageController = new TourImageController();
            CancelCommand = new RelayCommand(CancelButton_Click, CanExecute);
            CreateCommand = new RelayCommand(Button_Click_Kreiraj, CanExecute);
        }

        private string _url;

        public string Url
        {
            get => _url;
            set
            {
                if (value != _url)
                {
                    _url = value;
                    IsButtonEnabled = validateUrlRegex.IsMatch(Url);
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
            TourImage image = new TourImage();
            image.Url = Url;
            ImageController.Create(image);
            ImageController.Save();
        }

        private void CancelButton_Click(object param)
        {
            CloseWindow();
        }

        private bool CanExecute(object param) { return true; }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(EnterImage)) { window.Close(); }
            }
        }
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Url")
                {
                    if (string.IsNullOrEmpty(Url))
                        return "Enter a valid url!";

                }

                return null;
            }
        }


        private readonly string[] _validatedProperties = { "Url" };

        Regex validateUrlRegex = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");

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
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return validateUrlRegex.IsMatch(Url);
            }
        }

    }
}
