using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.View.CustomMessageBoxes;
using BookingProject.View.OwnersView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;
using System.Windows.Navigation;

namespace BookingProject.View.OwnersViewModel
{
    
    public class WizardAddImageViewModel
    {
        public Accommodation ForwardedAcc { get; set; }
        public NavigationService NavigationService { get; set; }
        public RelayCommand AddCommand { get; }
        public RelayCommand NextCommand { get; }
        public RelayCommand BackCommand { get; }
        public OwnerNotificationCustomBox OwnerCustomMessageBox { get; set; }
        public AccommodationImageController _imageController;
        public AccommodationImageController ImageController { get; set; }
        public WizardAddImageViewModel(Accommodation forwardedAcc, NavigationService navigationService) {
            NavigationService = navigationService;
            _imageController = new AccommodationImageController();
            ImageController = new AccommodationImageController();
            ForwardedAcc = forwardedAcc;
            AddCommand = new RelayCommand(Button_Click_Add, CanExecute);
            NextCommand = new RelayCommand(Button_Click_Next, CanExecute);
            BackCommand = new RelayCommand(Button_Click_Back, CanExecute);
            OwnerCustomMessageBox = new OwnerNotificationCustomBox();
        }
        private void Button_Click_Back(object param)
        {
            NavigationService.GoBack();
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
                    OnPropertyChanged();
                }
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool isAdded = false;
        int numberOfPhotos = 0;
        public void Button_Click_Add(object param)
        {
            AccommodationImage image = new AccommodationImage();
            image.Url = Url;
            ForwardedAcc.Images.Add(image);
            if (image.Url.IsEmpty())
            {
                OwnerCustomMessageBox.ShowCustomMessageBox("Photo url can not be empty!");
                return;
            }
            else
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
        public void Button_Click_Next(object param)
        {
            if (!isAdded) { OwnerCustomMessageBox.ShowCustomMessageBox("You need to add at least one image!"); return; }
            OwnerCustomMessageBox.ShowCustomMessageBox("You have added " + numberOfPhotos + " images!");
            ImageController.LinkToAccommodation(ForwardedAcc.Id);
            ImageController.SaveImage();
            NavigationService.Navigate(new WizardFinalConfirmationView(ForwardedAcc, NavigationService));
        }

    }
}
