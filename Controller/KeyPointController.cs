using BookingProject.FileHandler;
using BookingProject.Model;
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
            Load();
        }

        public void Load()
        {
            _keyPoints = _keyPointHandler.Load();
        }

        public List<KeyPoint> GetAll()
        {
            return _keyPoints;
        }

        public KeyPoint GetByID(int id)
        {
            return _keyPoints.Find(keyPoint => keyPoint.Id == id);
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
    }
}

