﻿using BookingProject.FileHandler;
using BookingProject.Model;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class LocationController : ISubject
    {
        private readonly List<IObserver> observers;

        private readonly LocationHandler _locationHandler;

        private List<Location> _locations;

        public LocationController()
        {
            _locationHandler = new LocationHandler();
            _locations = new List<Location>();
            Load();
        }

        public void Load()
        {
            _locations = _locationHandler.Load();
        }

        public void Create(Location location)
        {
            _locations.Add(location);
        }

        public void Save()
        {
            _locationHandler.Save(_locations);
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
