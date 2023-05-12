using BookingProject.Controller;
using BookingProject.Model.Enums;
using BookingProject.Model.Images;
using BookingProject.Model;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using BookingProject.Commands;
using System.Windows.Threading;
using System.Windows.Threading;
namespace BookingProject.View.GuideViewModel
{
    public class TourCreationViewModel: INotifyPropertyChanged, IDataErrorInfo
    {
        public ObservableCollection<LanguageEnum> Languages { get; set; }
        public TourController TourController { get; set; }
        public TourTimeInstanceController TourTimeInstanceController { get; set; }
        public TourLocationController LocationController { get; set; }
        public KeyPointController KeyPointController { get; set; }
        public TourStartingTimeController StartingDateController { get; set; }
        public TourImageController ImageController { get; set; }
        public UserController UserController { get; set; }
        public LanguageEnum ChosenLanguage { get; set; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand CreateCommand { get; }
        public RelayCommand ImageCommand { get; }
        public RelayCommand DateCommand { get; }
        public RelayCommand KeyPointCommand { get; }


        private DispatcherTimer _validationTimer;
        public bool IsEnabled { get; }

        public TourCreationViewModel()
        {
            var languages = Enum.GetValues(typeof(LanguageEnum)).Cast<LanguageEnum>();
            Languages = new ObservableCollection<LanguageEnum>(languages);
            TourController = new TourController();
            LocationController = new TourLocationController();
            KeyPointController = new KeyPointController();
            ImageController = new TourImageController();
            StartingDateController = new TourStartingTimeController();
            TourTimeInstanceController = new TourTimeInstanceController();
            UserController = new UserController();
            KeyPointCommand = new RelayCommand(Button_Click_KeyPoint, CanExecute);
            ImageCommand = new RelayCommand(Button_Click_Image, CanExecute);
            DateCommand = new RelayCommand(Button_Click_StartingTime, CanExecute);
            CancelCommand = new RelayCommand(CancelButton_Click, CanExecute);
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
        IsValid=FullValid(); // Call your validation method
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
        private string _tourLanguage;
        public string TourLanguage
        {
            get => _tourLanguage;
            set
            {
                if (value != _tourLanguage)
                {
                    _tourLanguage = value;
                    IsValid = FullValid();
                    OnPropertyChanged();
                }
            }
        }
        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
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
        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    IsValid = FullValid();
                    OnPropertyChanged();
                }
            }
        }
        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
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
            IsValid= FullValid();
            if (IsValid == false) { return; }
            Tour tour = new Tour();
            tour.Name = TourName;
            tour.Description = Description;
            tour.MaxGuests = MaxGuests;
            tour.DurationInHours = Duration;
            tour.Language = ChosenLanguage;
            tour.GuideId = UserController.GetLoggedUser().Id;
            Location location = new Location();
            location.City = City;
            location.Country = Country;
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
                instance.TourTime= time;
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
            MyToursWindow myToursWindow = new MyToursWindow();
            myToursWindow.Show();
            StopValidationTimer();
            CloseWindow();
        }
        private void Button_Click_StartingTime(object param)
        {
            EnterDate enterDate = new EnterDate();
            enterDate.Show();
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
                    if (string.IsNullOrEmpty(TourName))
                        return "You must enter a name!";

                }
                else if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        return "You must enter a description!";
                }
                else if (columnName == "Country")
                {
                    if (string.IsNullOrEmpty(Country))
                        return "You must enter a country!";
                }
                else if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "You must enter a city!";
                }

                else if (columnName == "Duration")
                {
                    if (!ValidDuration())
                        return "You must enter a number!";
                }

                else if (columnName == "MaxGuests")
                {
                    if (!ValidMaxGuests())
                        return "You must enter a number!";
                }
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "TourName", "Description", "Country", "City", "Duration", "MaxGuests" };
        public bool ValidDuration()
        {
            string testString = Duration.ToString();

            if (string.IsNullOrEmpty(testString) || Duration == 0)
            {
                return false;
            }
            return true;
        }
        public bool ValidMaxGuests()
        {
            string testString = MaxGuests.ToString();

            if (string.IsNullOrEmpty(testString) || MaxGuests == 0)
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

            return (ValidMaxGuests() && ValidDuration() && ValidKeyPoint() && ValidTourDateTime() && ValidTourImage());
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
                if (window.GetType() == typeof(TourCreationWindow)) { window.Close(); }
            }
        }
    }
}

