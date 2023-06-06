using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.ConversionHelp;
using BookingProject.Model.Images;
using BookingProject.View.CustomMessageBoxes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.WebPages;
using System.Windows;
using System.Windows.Navigation;

namespace BookingProject.View.OwnerViewModel
{
    public class AddPhotosToAccommodationViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        public AccommodationImageController _imageController;
        public RelayCommand AddCommand { get; }
        public RelayCommand MenuCommand { get; }
        public NavigationService NavigationService { get; set; }
        public RelayCommand BackCommand { get; set; }
        public OwnerNotificationCustomBox box { get; set; }

        public AddPhotosToAccommodationViewModel(NavigationService navigationService)
        {
            
            //_imageController = app.AccommodationImageController;
            _imageController = new AccommodationImageController();
            AddCommand = new RelayCommand(Button_Click_Add, CanExecute);
            MenuCommand = new RelayCommand(Button_Click_Menu, CanExecute);
            box = new OwnerNotificationCustomBox();
            BackCommand = new RelayCommand(Button_Click_Back, CanExecute);
            NavigationService = navigationService;
        }
        private void Button_Click_Menu(object param)
        {
            MenuView view = new MenuView();
            view.Show();
            CloseWindow();
        }
        private bool CanExecute(object param) { return true; }
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
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Url")
                {
                    if (string.IsNullOrEmpty(Url))
                        return ""; 
                    if (!validateUrlRegex.IsMatch(Url))
                    {
                        return "Url should be of format http(s)://something.extension";
                    }

                }

                return null;
            }
        }


        private readonly string[] _validatedProperties = { "Url" };

        Regex validateUrlRegex = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.(png|jpe?g)$");


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
        private string _displayedUrl;
        public string DisplayedUrl
        {
            get => _displayedUrl;
            set
            {
                if (value != _displayedUrl)
                {
                    _displayedUrl = value;
                    OnPropertyChanged();
                }
            }
        }
        private void Button_Click_Back(object param)
        {
            //var view = new OwnerssView();
            //view.Show();
            //CloseWindow();
            if (!isAdded) { box.ShowCustomMessageBox("You need to add at least one image!"); return; }
            box.ShowCustomMessageBox("You have added " + numberOfPhotos + " images!");
            NavigationService.GoBack();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool isAdded = false;
        private int numberOfPhotos = 0;
        public void Button_Click_Add(object param)
        {
            AccommodationImage image = new AccommodationImage();
            image.Url = Url;
            if (image.Url.IsEmpty())
            {
                box.ShowCustomMessageBox("Photo url can not be empty!");
                return;
            } else
            {
                numberOfPhotos++;
                isAdded = true;
                _imageController.Create(image);
                Url = string.Empty;
                //OnPropertyChanged(nameof(Url));
                //TextBoxClearHelper.ClearTextBox(param);
                //NavigationService.Refresh();
                DisplayedUrl = image.Url;
            }
            

            //_imageController.SaveImage();
        }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(AddPhotosToAccommodationView)) { window.Close(); }
            }
        }
    }
}
