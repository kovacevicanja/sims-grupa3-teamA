using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.Model.Images;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.Controller
{
    public class TourController : ISubject
    {
        private readonly List<IObserver> observers;

        private readonly TourHandler _tourHandler;

        private List<Tour> _tours;

        private TourLocationController _locationController;

        private TourImageController _tourImageController;

        private KeyPointController _keyPointController;

        private TourStartingTimeController _startingDateController;

        public TourController()
        {
            _tourHandler = new TourHandler();
            _tours = new List<Tour>();
            _locationController = new TourLocationController();
            _tourImageController = new TourImageController();
            _keyPointController = new KeyPointController();
            _startingDateController = new TourStartingTimeController();
            Load();
        }

        public void Load()
        {
            _tours = _tourHandler.Load();
            TourLocationBind();
            TourImageBind();
            TourDateBind(); 
            TourKeyPointBind(); 
        }

        private int GenerateId()
        {
            int maxId = 0;
            foreach (Tour tour in _tours)
            {
                if (tour.Id > maxId)
                {
                    maxId = tour.Id;
                }
            }
            return maxId + 1;
        }

        public void Create(Tour tour)
        {
            tour.Id = GenerateId(); 
            _tours.Add(tour);
        }

        public void Save()
        {
            _tourHandler.Save(_tours);
        }


        public List<Tour> GetAll()
        {
            return _tours;
        }

        public Tour GetByID(int id)
        {
            return _tours.Find(tour => tour.Id == id);
        }

        public void TourLocationBind()
        {
            _locationController.Load();
            foreach (Tour tour in _tours)
            {
                Location location = _locationController.GetByID(tour.LocationId);
                tour.Location = location;
            }
        }
        //TourImage bind
        public List <TourImage> LoadImages ()
        {
            TourImageHandler tourImageHandler = new TourImageHandler();
            return tourImageHandler.Load();
        }
        public void AddImagesToTour (Tour tour, List <TourImage> images)
        {
            foreach (TourImage image in images)
            {
                if (tour.Id == image.TourId)
                {
                    tour.Images.Add(image);
                }

            }
        }
        public void BindTourImage (List<TourImage> images)
        {
            foreach (Tour tour in _tours)
            {
                AddImagesToTour(tour, images);
            }
        }
        public void TourImageBind()
        {
            List<TourImage> images = LoadImages();
            BindTourImage(images);
        }
        //
        //TourKeyPoint bind
        public List <KeyPoint> LoadKeyPoints()
        {
            KeyPointHandler keyPointHandler = new KeyPointHandler();
            return keyPointHandler.Load();
        }

        public void AddKeyPointToTour(Tour tour, List<KeyPoint> keyPoints)
        {
            foreach (KeyPoint keyPoint in keyPoints)
            {
                if (tour.Id == keyPoint.TourId)
                {
                    tour.KeyPoints.Add(keyPoint);
                }
            }
        }

        public void BindTourKeyPoint (List <KeyPoint> keyPoints)
        {
            foreach (Tour tour in _tours)
            {
                AddKeyPointToTour(tour, keyPoints);
            }
        }

        public void TourKeyPointBind()
        {
            List<KeyPoint> keyPoints = LoadKeyPoints();
            BindTourKeyPoint(keyPoints);
        }
        //
        //TourDate bind
        public void AddStartingTimesToTour(Tour tour, List<TourDateTime> dates)
        {
            foreach (TourDateTime date in dates)
            {
                if (tour.Id == date.TourId)
                {
                    tour.StartingTime.Add(date);
                }
            }
        }
        public void BindTourStartingTimes(List<TourDateTime> dates)
        {
            foreach (Tour tour in _tours)
            {
                AddStartingTimesToTour(tour, dates);
            }
        }
        public List<TourDateTime> LoadTourStartingTimes()
        {
            TourStartingTimeHandler dateHandler = new TourStartingTimeHandler();
            return dateHandler.Load();
        }
        public void TourDateBind()
        {
            List<TourDateTime> dates = LoadTourStartingTimes();
            BindTourStartingTimes(dates);
        }
        //
        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }
        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }

        public bool WantedTour (Tour tour, string city, string country, string duration, string choosenLanguage, string numOfGuest)
        {
            string languageEnum = tour.Language.ToString().ToLower();

            if ((city.Equals("") || tour.Location.City.ToLower().Contains(city.ToLower()))
                    && (country.Equals("") || tour.Location.Country.ToLower().Contains(country.ToLower()))
                    && (duration.Equals("") || double.Parse(duration) == tour.DurationInHours)
                    && (choosenLanguage.Equals("") || languageEnum.Equals(choosenLanguage.ToLower()))
                    && (numOfGuest.Equals("") || int.Parse(numOfGuest) <= tour.MaxGuests))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ObservableCollection<Tour> Search(ObservableCollection<Tour> tourView, string city, string country, string duration, string choosenLanguage, string numOfGuest)
        {
            tourView.Clear();

            foreach (Tour tour in _tours)
            {
                if (WantedTour(tour, city, country, duration, choosenLanguage, numOfGuest))
                {
                    tourView.Add(tour);
                }
            }
            return tourView;
        }

        public void ShowAll(ObservableCollection<Tour> tourView)
        {
            tourView.Clear();

            foreach (Tour tour in _tours)
            {
                tourView.Add(tour);
            }
        }
    }
}
