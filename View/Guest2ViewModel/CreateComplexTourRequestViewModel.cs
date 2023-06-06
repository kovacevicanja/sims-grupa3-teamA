using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Domain.Enums;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using BookingProject.Validation;
using BookingProject.View.CustomMessageBoxes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingProject.View.Guest2ViewModel
{
    public class CreateComplexTourRequestViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<LanguageEnum> Languages { get; set; }
        public LanguageEnum ChosenLanguage { get; set; }
        public TourLocationController _tourLocationController { get; set; }
        public TourRequestController _tourRequestController { get; set; }
        public UserController _userController { get; set; }
        public int GuestId { get; set; }
        public CustomMessageBox CustomMessageBox;
        public RelayCommand CancelCommand { get; }
        public RelayCommand CreateTourRequestCommand { get; }
        public User User { get; set; }
        public int Flag { get; set; }
        public CorrectInputCityValidationRule correctInputCityvalidationRule { get; set; }
        public NavigationService NavigationService { get; set; }
        public ComplexTourRequestController _complexTourRequestController { get; set; }
        public ComplexTourRequest ComplexTourRequest { get; set; }  
        public CustomMessageBoxComplexTourRequests CustomMessageBoxComplexTourRequests { get; set; }

        public CreateComplexTourRequestViewModel(int guestId, NavigationService navigationService)
        {
            _complexTourRequestController = new ComplexTourRequestController();
            ComplexTourRequest = new ComplexTourRequest();

            ComplexTourRequest.Id = Injector.CreateInstance<IComplexTourRequestRepository>().GenerateId();

            CustomMessageBoxComplexTourRequests = new CustomMessageBoxComplexTourRequests();

            //ideja napraviti meessage box koji pita da li je to to sto se tice unosa ili zelite jos tura da unesete?
            //onda on ako klikne da, zavrsava se unos i vraca se na prethodnu stranicu, ako klikne ne onda nastavkja da unosi jos tura *u zavisnosti od kontektsa*
            //zahtevi se dodaju u listu zahteva, i svakom zahtevu se nalepi id tour requesta, ako slucajno korisnik odustane, moraju se obrisati 
            //i complexan zahtev (kao za sliku u recenziji --- pogledati)

            var languages = Enum.GetValues(typeof(LanguageEnum)).Cast<LanguageEnum>();
            Languages = new ObservableCollection<LanguageEnum>(languages);

            NavigationService = navigationService;

            _tourRequestController = new TourRequestController();
            _tourLocationController = new TourLocationController();
            _userController = new UserController();

            Flag = 0;

            GuestId = guestId;
            CustomMessageBox = new CustomMessageBox();

            StartDate = DateTime.Today;
            EndDate = DateTime.Today;

            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
            CreateTourRequestCommand = new RelayCommand(Button_Click_CreateTourRequest, CanExecute);

            User = new User();

            correctInputCityvalidationRule = new CorrectInputCityValidationRule();
        }

        private bool CanExecute(object param) { return true; }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _tourLanguage;
        public string TourLanguage
        {
            get => _tourLanguage;
            set
            {
                if (value != _tourLanguage)
                {
                    _tourLanguage = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _guestsNumber;
        public int GuestsNumber
        {
            get => _guestsNumber;
            set
            {
                if (value != _guestsNumber)
                {
                    _guestsNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_CreateTourRequest(object param)
        {
            TourRequest tourRequest = new TourRequest();

            Location location = new Location();

            location.City = City;
            location.Country = Country;

            tourRequest.Description = Description;
            tourRequest.StartDate = StartDate;
            tourRequest.EndDate = EndDate;
            tourRequest.Language = ChosenLanguage;
            tourRequest.GuestsNumber = GuestsNumber;
            tourRequest.Status = Domain.Enums.TourRequestStatus.PENDING;
            tourRequest.Guest.Id = GuestId;
            tourRequest.ComplexTour.Id = ComplexTourRequest.Id;

            ValidationResult resultCity = correctInputCityvalidationRule.Validate(location.City, CultureInfo.CurrentCulture);

            if (string.IsNullOrEmpty(location.City) || string.IsNullOrEmpty(location.Country) || (tourRequest.StartDate == DateTime.Today
                && tourRequest.EndDate == DateTime.Today) || tourRequest.GuestsNumber == 0)
            {
                CustomMessageBox.ShowCustomMessageBox("You can not create a tour request. Some of the required fields are not filled out.");
            }
            else
            {
                if (!(location.City.IsEmpty() || location.Country.IsEmpty()) && resultCity.IsValid)
                {
                    _tourLocationController.Create(location);
                    tourRequest.Location = location;

                    _tourRequestController.Create(tourRequest);


                    ComplexTourRequest.TourRequestsList.Add(tourRequest);

                    string MessageText = "Do you want to continue adding more tour requests to complex tours? If you want to - press continue, if you are done - press finish.";
                    bool resultFlag;
                    CustomMessageBoxComplexTourRequests.ShowCustomMessageBoxComplexTourRequests(MessageText, out resultFlag);

                    if (resultFlag)
                    {
                        City = "";
                        Country = "";
                        Description = "";
                        GuestsNumber = 0;
                        StartDate = DateTime.Today;
                        EndDate = DateTime.Today;

                        //ovde nastavljam sa dodavanjem tura.
                        // Continue based on true value -> ako je true onda nastavljamo sa dodavanjem novih zahteva za ture
                    }
                    else
                    {
                        ComplexTourRequest.Status = TourRequestStatus.PENDING;
                        ComplexTourRequest.Guest.Id = GuestId;
                        _complexTourRequestController.Create(ComplexTourRequest);
                        NavigationService.GoBack();
                    }
                    City = "";
                    Country = "";
                    Description = "";
                    GuestsNumber = 0;
                    StartDate = DateTime.Today;
                    EndDate = DateTime.Today;
                }
                else
                {
                    MessageBox.Show("You have not entered the correct city or country.");
                }
            }

        }

        private void Button_Click_Cancel(object param)
        {
            _complexTourRequestController.DeleteRequestIfComplexRequestNotCreated(ComplexTourRequest.Id);
            NavigationService.GoBack();
        }
    }
}