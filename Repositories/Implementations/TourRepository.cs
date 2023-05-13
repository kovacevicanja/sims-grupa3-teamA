using BookingProject.Controller;
using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories
{
    public class TourRepository : ITourRepository
    {
        private const string FilePath = "../../Resources/Data/tours.csv";
        private Serializer<Tour> _serializer;
        public List<Tour> _tours;
        public ITourStartingTimeRepository _tourStartingTimeRepository;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = Load();
        }
        public void Initialize()
        {
            _tourStartingTimeRepository = Injector.CreateInstance<ITourStartingTimeRepository>();
            TourLocationBind();
            BindTourImage();
            TourKeyPointBind();
            TourDateBind();
        }
        public List<Tour> Load()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save(List<Tour> tours)
        {
            _serializer.ToCSV(FilePath, tours);
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
            Save(_tours);
        }

        public void BindLastTour()
        {
            
             if (_tours.Count == 0)
             {
                 return;
             }
             else
             {
                 Tour tour = _tours.Last();
                List<TourDateTime> dates = Injector.CreateInstance<ITourStartingTimeRepository>().GetAll();
                List<KeyPoint> keyPoints = Injector.CreateInstance<IKeyPointRepository>().GetAll();
                List<TourImage> images = Injector.CreateInstance<ITourImageRepository>().GetAll();
                List<Location> locations = Injector.CreateInstance<ITourLocationRepository>().GetAll();
                foreach (TourDateTime date in dates)
                 {
                     if (tour.Id == date.TourId)
                     {
                         tour.StartingTime.Add(date);
                     }
                 }

                foreach (KeyPoint keyPoint in keyPoints)
                {
                    if (tour.Id == keyPoint.TourId)
                    {
                        tour.KeyPoints.Add(keyPoint);
                    }
                }

                foreach (TourImage image in images)
                {
                    if (tour.Id == image.Tour.Id)
                    {
                        tour.Images.Add(image);
                    }
                }

                foreach (Location location in locations)
                {
                    if (tour.LocationId == location.Id)
                    {
                        tour.Location=location;
                    }
                }

            }
         }

            public List<Tour> GetAll()
        {
            return _tours.ToList();
        }
        public Tour GetById(int id)
        {
            return _tours.Find(tour => tour.Id == id);
        }
        public void TourLocationBind()
        {
            foreach (Tour tour in _tours)
            {
                Location location = Injector.CreateInstance<ITourLocationRepository>().GetById(tour.LocationId);
                tour.Location = location;
            }
        }
        public void BindTourImage()
        {
            ITourImageRepository tourImageRepository = Injector.CreateInstance<ITourImageRepository>();
            foreach (TourImage image in tourImageRepository.GetAll())
            {
                Tour tour = GetById(image.Tour.Id);
                tour.Images.Add(image);

            }
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
        public void BindTourKeyPoint(List<KeyPoint> keyPoints)
        {
            foreach (Tour tour in _tours)
            {
                AddKeyPointToTour(tour, keyPoints);
            }
        }
        public void TourKeyPointBind()
        {
            List<KeyPoint> keyPoints = Injector.CreateInstance<IKeyPointRepository>().GetAll();
            BindTourKeyPoint(keyPoints);
        }
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
        public void TourDateBind()
        {
            List<TourDateTime> dates = Injector.CreateInstance<ITourStartingTimeRepository>().GetAll();
            BindTourStartingTimes(dates);
        }
    }
}