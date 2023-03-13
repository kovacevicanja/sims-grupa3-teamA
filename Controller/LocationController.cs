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
    public class LocationController : ISubject
    {
        private readonly  List<IObserver> observers;

        private readonly LocationHandler _locationHandler;

        private List<Location> _locations;
        private Serializer<Location> serializer;
        private readonly string fileName = "../../Resources/Data/locations.csv";

        public LocationController()
        {
            serializer = new Serializer<Location>();
            observers = new List<IObserver>();
            _locationHandler = new LocationHandler();
            _locations = new List<Location>();
            Load();
        }
        public Location Create(Location location)
        {
            //AddLocation(location);
            location.Id = GenerateId();
            _locations.Add(location);
            SaveLocation();
            NotifyObservers();
            return location;
        }
        //public Location AddLocation(Location location)
        //{
        //    location.Id = GenerateId();
        //    _locations.Add(location);
        //    SaveLocation();
        //    NotifyObservers();
        //    return location;
        //}

        public int GenerateId()
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
        private void SaveLocation()
        {
            _locationHandler.Save(_locations);
        }

        public void Load()
        {
            _locations = _locationHandler.Load(); 
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
