using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.View;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class KeyPointController : ISubject
    {
        private readonly List<IObserver> observers;

        private readonly KeyPointHandler _keyPointHandler;

        private List<KeyPoint> _keyPoints;

        public KeyPointController()
        {
            _keyPointHandler = new KeyPointHandler();
            _keyPoints = new List<KeyPoint>();
            observers = new List<IObserver>();
            Load();
        }

        public void Load()
        {
            _keyPoints = _keyPointHandler.Load();
        }


        private int GenerateId()
        {
            int maxId = 0;
            foreach (KeyPoint keyPoint in _keyPoints)
            {
                if (keyPoint.Id > maxId)
                {
                    maxId = keyPoint.Id;
                }
            }
            return maxId + 1;
        }

        public void Create(KeyPoint keyPoint)
        {
            keyPoint.Id = GenerateId();
            _keyPoints.Add(keyPoint);
            NotifyObservers();
        }

        public void Save()
        {
            _keyPointHandler.Save(_keyPoints);
            NotifyObservers();
        }

        public List<KeyPoint> GetAll()
        {
            return _keyPoints;
        }

        public KeyPoint GetByID(int id)
        {
            return _keyPoints.Find(keyPoint => keyPoint.Id == id);
        }

        public void CleanUnused()
        {
            _keyPoints.RemoveAll(r => r.TourId == -1);
            NotifyObservers();
        }

        public void LinkToTour(int id)
        {

            foreach(KeyPoint keyPoint in _keyPoints)
            {
                if (keyPoint.TourId == -1)
                {
                    keyPoint.TourId = id;
                }

            }
            NotifyObservers();
        }

        public List<KeyPoint> GetToursKeyPoints(int id)
        {
            List <KeyPoint> tourKeyPoints = new List<KeyPoint>();  

            foreach (KeyPoint kp in _keyPoints)
            {
                if (kp.TourId == id)
                {
                    tourKeyPoints.Add(kp);
                }
            }
            return tourKeyPoints;
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

        internal void Subscribe(LiveTourView liveTourView)
        {
            throw new NotImplementedException();
        }
    }
}

