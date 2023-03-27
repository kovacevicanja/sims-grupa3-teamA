using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.Serializer;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Annotations;

namespace BookingProject.Controller
{
    public class TourLocationController : ISubject
    {
        private readonly  List<IObserver> observers;

        private readonly TourLocationHandler _locationHandler;

        private List<Location> _locations;

        public TourLocationController()
        {
            _locationHandler = new TourLocationHandler();
            _locations = new List<Location>();
            observers = new List<IObserver>();
            Load();
        }

        public void Load()
        {
            _locations = _locationHandler.Load();
        }

        public void Save()
        {
            _locationHandler.Save(_locations);
            NotifyObservers();
        }

        private int GenerateId()
        {
            int maxId = 0;
            foreach (Location location in _locations)
            {
                if (location.Id > maxId)
                {
                    maxId = location.Id;
                }
            }
            return maxId + 1;
        }

        public void Create(Location location)
        {
            location.Id = GenerateId();
            _locations.Add(location);
            NotifyObservers();
        }

        public List<Location> GetAll()
        {
            return _locations;
        }

        public Location GetByID(int id)
        {
            return _locations.Find(location => location.Id == id);
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
