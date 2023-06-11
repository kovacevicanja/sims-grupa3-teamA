using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model.Enums;
using BookingProject.Model.Images;
using BookingProject.Model;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using BookingProject.Domain;
using BookingProject.Controllers;
using BookingProject.Localization;

namespace BookingProject.View.GuideViewModel
{

    public class RequestedTourCreationViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public ObservableCollection<LanguageEnum> Languages { get; set; }
        public TourController TourController { get; set; }
        public TourTimeInstanceController TourTimeInstanceController { get; set; }
        public TourLocationController LocationController { get; set; }
        public KeyPointController KeyPointController { get; set; }
        public TourStartingTimeController StartingDateController { get; set; }
        public TourRequestController TourRequestController { get; set; }
        public TourImageController ImageController { get; set; }
        public UserController UserController { get; set; }
        public LanguageEnum ChosenLanguage { get; set; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand CreateCommand { get; }
        public RelayCommand DetailsCommand { get; }
        public RelayCommand ImageCommand { get; }
        public RelayCommand DateCommand { get; }
        public RelayCommand KeyPointCommand { get; }

        public TourRequest ChosenRequest{get;}


        private DispatcherTimer _validationTimer;
        public bool IsEnabled { get; }

        public RequestedTourCreationViewModel(TourRequest chosenRequest)
        {
            ChosenRequest= chosenRequest;
            var languages = Enum.GetValues(typeof(LanguageEnum)).Cast<LanguageEnum>();
            Languages = new ObservableCollection<LanguageEnum>(languages);
            TourController = new TourController();
            LocationController = new TourLocationController();
            KeyPointController = new KeyPointController();
            ImageController = new TourImageController();
            TourRequestController = new TourRequestController();
            StartingDateController = new TourStartingTimeController();
            TourTimeInstanceController = new TourTimeInstanceController();
            UserController = new UserController();
            KeyPointCommand = new RelayCommand(Button_Click_KeyPoint, CanExecute);
            ImageCommand = new RelayCommand(Button_Click_Image, CanExecute);
            DateCommand = new RelayCommand(Button_Click_StartingTime, CanExecute);
            CancelCommand = new RelayCommand(CancelButton_Click, CanExecute);
            DetailsCommand = new RelayCommand(DetailsButton_Click, CanExecute);
            CreateCommand = new RelayCommand(Button_Click_Kreiraj, CanExecute);
            _validationTimer = new DispatcherTimer();
            _validationTimer.Interval = TimeSpan.FromSeconds(1); // Adjust the interval as needed
            _validationTimer.Tick += ValidationTimer_Tick;
            StartValidationTimer();
            IsValid = FullValid();
        }

        private string _tourName;
        public string TourName
        {
            get => _tourName;
            set
            {
                if (value != _tourName)
                {
                    _tourName = value;
                    IsValid = FullValid();
                    OnPropertyChanged();
                }
            }
        }
        private void ValidationTimer_Tick(object sender, EventArgs e)
        {
            IsValid = FullValid(); // Call your validation method
        }
        private void StartValidationTimer()
        {
            _validationTimer.Start();
        }
        public void StopValidationTimer()
        {
            _validationTimer.Stop();
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
                    IsValid = FullValid();
                    OnPropertyChanged();
                }
            }
        }
        private double _duration;
        public double Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    IsValid = FullValid();
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
            IsValid = FullValid();
            if (IsValid == false) { return; }
            Tour tour = new Tour();
            tour.Name = TourName;
            tour.ComplexTourRequestId = ChosenRequest.ComplexTourRequestId;
            tour.Description = Description;
            tour.MaxGuests = ChosenRequest.GuestsNumber;
            tour.DurationInHours = Duration;
            tour.Language = ChosenRequest.Language;
            tour.GuideId = UserController.GetLoggedUser().Id;
            Location location = new Location();
            location.City = ChosenRequest.Location.City;
            location.Country = ChosenRequest.Location.Country;
            //uvezivanje sa lokacijom
            LocationController.Create(location);
            LocationController.Save();
            tour.LocationId = location.Id;
            //kreiranje
            TourController.Create(tour);
            //uvezivanje sa ostalim pomocnim klasama
            KeyPointController.LinkToTour(tour.Id);
            KeyPointController.Save();
            ImageController.LinkToTour(tour.Id);
            ImageController.Save();
            StartingDateController.LinkToTour(tour.Id);
            StartingDateController.Save();
            //prave se instance
            TourController.BindLastTour();
            SaveInstance();
            sendINFO(TourController.GetLastTour());
        }

        public void sendINFO(Tour tour)
        {
            TourRequestController.GetById(ChosenRequest.Id).Status = Domain.Enums.TourRequestStatus.ACCEPTED;
            TourRequestController.GetById(ChosenRequest.Id).GuideId = UserController.GetLoggedUser().Id;
            TourRequestController.GetById(ChosenRequest.Id).SetDate = tour.StartingTime[0].StartingDateTime;
            TourRequestController.SaveTourRequest();
            TourRequestController.SendNotification(ChosenRequest.Guest, tour);
        }
        public void SaveInstance()
        {
            MakeTimeInstances(TourController.GetLastTour());

        }
        //use of this function is necessary if a tour has multiple dates   
        public void MakeTimeInstances(Tour tour)
        {
            foreach (TourDateTime time in tour.StartingTime)
            {
                TourTimeInstance instance = new TourTimeInstance();
                instance.TourId = tour.Id;
                instance.Tour = tour;
                instance.DateId = time.Id;
                instance.TourTime = time;
                TourTimeInstanceController.Create(instance);
                TourTimeInstanceController.Save();
            }
        }
        private void CancelButton_Click(object param)
        {
            StartingDateController.CleanUnused();
            StartingDateController.Save();
            KeyPointController.CleanUnused();
            KeyPointController.Save();
            ImageController.CleanUnused();
            ImageController.Save();
            AllTourRequestsView allTourRequestsView = new AllTourRequestsView();
            allTourRequestsView.Show();
            StopValidationTimer();
            CloseWindow();
        }
        private void DetailsButton_Click(object param)
        {
            RequestDetailsView view = new RequestDetailsView(ChosenRequest);
            view.Show();
        }
        private void Button_Click_StartingTime(object param)
        {
            EnterRequestDate enterRequestDate = new EnterRequestDate(ChosenRequest);
            enterRequestDate.Show();
            IsValid = FullValid();
        }
        private void Button_Click_KeyPoint(object param)
        {
            EnterKeyPoint enterKeyPoint = new EnterKeyPoint();
            enterKeyPoint.Show();
            IsValid = FullValid();
        }
        private void Button_Click_Image(object param)
        {
            EnterImage enterImage = new EnterImage();
            enterImage.Show();
            IsValid = FullValid();
        }
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "TourName")
                {
                    if ((TranslationSource.Instance.CurrentCulture.Name).Equals("en-US"))
                    {
                        if (string.IsNullOrEmpty(TourName))
                            return "You must enter a name!";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(TourName))
                            return "Morate uneti ime!";

                    }

                }
                else if (columnName == "Description")
                {
                    if ((TranslationSource.Instance.CurrentCulture.Name).Equals("en-US"))
                    {
                        if (string.IsNullOrEmpty(Description))
                            return "You must enter a description!";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Description))
                            return "Morate uneti opis!";
                    }
                }
                else if (columnName == "Duration")
                {
                    if ((TranslationSource.Instance.CurrentCulture.Name).Equals("en-US"))
                    {
                        if (!ValidDuration())
                            return "You must enter a number!";
                    }
                    else
                    {
                        if (!ValidDuration())
                            return "Morate uneti broj!";
                    }
                }
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "TourName", "Description", "Duration" };
        public bool ValidDuration()
        {
            string testString = Duration.ToString();

            if (string.IsNullOrEmpty(testString) || Duration == 0)
            {
                return false;
            }
            return true;
        }

        public bool ValidKeyPoint()
        {
            int keyPointNumber = 0;
            KeyPointController keyPointController = new KeyPointController();
            foreach (KeyPoint keyPoint in KeyPointController.GetAll())
            {
                if (keyPoint.TourId == -1)
                {
                    keyPointNumber++;
                }
            }
            if (keyPointNumber < 2)
            {
                return false;
            }
            return true;
        }
        public bool ValidTourImage()
        {
            TourImageController tourImageController = new TourImageController();
            foreach (TourImage tourImage in tourImageController.GetAll())
            {
                if (tourImage.Tour.Id == -1)
                {
                    return true;
                }
            }
            return false;
        }
        public bool ValidTourDateTime()
        {
            TourStartingTimeController tourStartingTimeController = new TourStartingTimeController();
            foreach (TourDateTime tourDate in tourStartingTimeController.GetAll())
            {
                if (tourDate.TourId == -1)
                {
                    return true;
                }
            }
            return false;
        }
        public bool FullValid()
        {
            foreach (var property in _validatedProperties)
            {
                if (this[property] != null)
                    return false;
            }

            return (ValidDuration() && ValidKeyPoint() && ValidTourDateTime() && ValidTourImage());
        }

        private bool _isValid = false;
        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                value = FullValid();
                if (_isValid != value)
                {
                    _isValid = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(RequestedTourCreation)) { window.Close(); }
            }
        }
    }
}