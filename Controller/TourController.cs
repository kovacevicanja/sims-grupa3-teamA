﻿using BookingProject.FileHandler;
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

        private LocationController _locationController;

        private TourImageController _tourImageController;

        private KeyPointController _keyPointController;

        private StartingDateController _startingDateController;

        public TourController()
        {
            _tourHandler = new TourHandler();
            _tours = new List<Tour>();
            _locationController = new LocationController();
            _tourImageController = new TourImageController();
            _keyPointController = new KeyPointController();
            _startingDateController = new StartingDateController();
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

        public void TourImageBind()
        {

            List<TourImage> images = new List<TourImage>();
            TourImageHandler imageHandler = new TourImageHandler();
            images = imageHandler.Load();

            foreach (Tour tour in _tours)
            {
                foreach (TourImage image in images)
                {

                    if (tour.Id == image.TourId)
                    {
                        tour.Images.Add(image);
                    }

                }
            }
        }


        public void TourKeyPointBind()
        {
            List<KeyPoint> keyPoints = new List<KeyPoint>();
            KeyPointHandler keyPointHandler = new KeyPointHandler();
            keyPoints = keyPointHandler.Load();

            foreach (Tour tour in _tours)
            {
                foreach (KeyPoint keyPoint in keyPoints)
                {

                    if (tour.Id == keyPoint.TourId)
                    {
                        tour.KeyPoints.Add(keyPoint);
                    }

                }
            }
        }

        public void TourDateBind()
        {
            List<StartingDate> dates = new List<StartingDate>();
            StartingDateHandler dateHandler = new StartingDateHandler();
            dates = dateHandler.Load();

            foreach (Tour tour in _tours)
            {
                foreach (StartingDate date in dates)
                {

                    if (tour.Id == date.TourId)
                    {
                        tour.StartingTime.Add(date);
                    }

                }
            }
        }



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


        public ObservableCollection<Tour> Search(ObservableCollection<Tour> tourView, string city, string country, string duration, string choosenLanguage, string numOfGuest)
        {
            tourView.Clear();

            foreach (Tour tour in _tours)
            {
                string languageEnum = tour.Language.ToString().ToLower();

                bool isTour = (city.Equals("") || tour.Location.City.ToLower().Contains(city.ToLower()))
                    && (country.Equals("") || tour.Location.Country.ToLower().Contains(country.ToLower()))
                    && (duration.Equals("") || double.Parse(duration) == tour.DurationInHours)
                    && (choosenLanguage.Equals("") || languageEnum.Equals(choosenLanguage.ToLower()))
                    && (numOfGuest.Equals("") || int.Parse(numOfGuest) <= tour.MaxGuests);

                if (isTour)
                {
                    tourView.Add(tour);
                }
            }
            return tourView;
        }

        public ObservableCollection<Tour> TryToBook(ObservableCollection<Tour> tourReservation, Tour choosenTour, string numberOfGuest)
        {
            if (int.Parse(numberOfGuest) <= choosenTour.MaxGuests)
            {
                choosenTour.MaxGuests = choosenTour.MaxGuests - int.Parse(numberOfGuest); 
                tourReservation.Add(choosenTour);
            }
            else
            {
                MessageBox.Show("You have exceeded the maximum number of guests per tour!");
                //dalje ponudi mu neke slicne ture
            }

            return tourReservation;
        }
    }

}
