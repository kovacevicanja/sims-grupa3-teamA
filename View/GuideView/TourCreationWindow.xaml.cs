using BookingProject.Controller;
using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Model.Images;
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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for TourCreationView.xaml
    /// </summary>
    public partial class TourCreationWindow : Window, IDataErrorInfo
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

        public TourCreationWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            var languages = Enum.GetValues(typeof(LanguageEnum)).Cast<LanguageEnum>();
            Languages = new ObservableCollection<LanguageEnum>(languages);

            var app = Application.Current as App;
            TourController = app.TourController;
            LocationController = app.LocationController;
            KeyPointController = app.KeyPointController;
            ImageController = app.ImageController;
            StartingDateController = app.StartingDateController;
            TourTimeInstanceController = app.TourTimeInstanceController;
            UserController = app.UserController;
        }

        //public string this[string columnName] => throw new NotImplementedException();

        // public string Error => throw new NotImplementedException();

        private string _tourName;

        public string TourName
        {
            get => _tourName;
            set
            {
                if (value != _tourName)
                {
                    _tourName = value;
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

        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
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
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }







        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }







        private void Button_Click_Kreiraj(object sender, RoutedEventArgs e)
        {
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
            TourController.Save();


            //uvezivanje sa ostalim pomocnim klasama
            KeyPointController.LinkToTour(tour.Id);
            KeyPointController.Save();

            ImageController.LinkToTour(tour.Id);
            ImageController.Save();

            StartingDateController.LinkToTour(tour.Id);
            StartingDateController.Save();

            //prave se instance
            saveInstance();


        }


        public void saveInstance()
        {
            StartingDateController.Load();
            TourController.Load();
            makeTimeInstances(TourController.GetLastTour());
        }

        //use of this function is necessary if a tour has multiple dates   
        public void makeTimeInstances(Tour tour)
        {
            foreach(TourDateTime time in tour.StartingTime)
            {
                TourTimeInstance instance= new TourTimeInstance();
                instance.TourId = tour.Id;
                instance.DateId = time.Id;
                TourTimeInstanceController.Create(instance);
                TourTimeInstanceController.Save();
            }

        }



        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            KeyPointController.Load();
            KeyPointController.CleanUnused();
            KeyPointController.Save();

            ImageController.Load();
            ImageController.CleanUnused();
            ImageController.Save();

            StartingDateController.Load();
            StartingDateController.CleanUnused();
            StartingDateController.Save();
                
            MyToursWindow myToursWindow = new MyToursWindow();
            myToursWindow.Show();
            Close();

        }

        private void Button_Click_StartingTime(object sender, RoutedEventArgs e)
        {
            EnterDate enterDate = new EnterDate();
            enterDate.Show();

        }

        private void Button_Click_KeyPoint(object sender, RoutedEventArgs e)
        {
            EnterKeyPoint enterKeyPoint = new EnterKeyPoint();
            enterKeyPoint.Show();


        }

        private void Button_Click_Image(object sender, RoutedEventArgs e)
        {
            EnterImage enterImage = new EnterImage();
            enterImage.Show();

        }


        //private Regex _IndexRegex = new Regex("[A-Z]{2} [0-9]{1,3}/[0-9]{4}");

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
                    if (!validDuration())
                        return "You must enter a number!";
                }

                else if (columnName == "MaxGuests")
                {
                    if (!validMaxGuests())
                        return "You must enter a number!";
                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "TourName", "Description", "Country", "City", "Duration", "MaxGuests" };


        public bool validDuration()
        {

            string testString = Duration.ToString();

            if (string.IsNullOrEmpty(testString) || Duration == 0)
            {
                return false;
            }

            return true;

        }

        public bool validMaxGuests()
        {

            string testString = MaxGuests.ToString();

            if (string.IsNullOrEmpty(testString) || MaxGuests == 0)
            {
                return false;
            }

            return true;

        }

        public bool validKeyPoint()
        {
            int keyPointNumber = 0;
            KeyPointHandler keyPointHandler = new KeyPointHandler();
            foreach (KeyPoint keyPoint in keyPointHandler._keyPoints)
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

        public bool validTourImage()
        {
            TourImageHandler tourImageHandler = new TourImageHandler();
            foreach (TourImage tourImage in tourImageHandler._images)
            {
                if (tourImage.Tour.Id == -1)
                {
                    return true;
                }
            }
            return false;
        }

        public bool validTourDateTime()
        {

            TourStartingTimeHandler tourStartingTimeHandler = new TourStartingTimeHandler();
            foreach (TourDateTime tourDate in tourStartingTimeHandler._dates)
            {
                if (tourDate.TourId == -1)
                {
                    return true;
                }
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

                return (validMaxGuests() && validDuration() && validKeyPoint() && validTourDateTime() && validTourImage());

            }
        }

        private void Window_LayoutUpdated(object sender, System.EventArgs e)
        {
            if (IsValid)
                CreateButton.IsEnabled = true;
            else
                CreateButton.IsEnabled = false;
        }

    }
}
