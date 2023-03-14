using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.FileHandler;
using BookingProject.Model;
using OisisiProjekat.Observer;

namespace BookingProject.Controller
{
    internal class AccommodationDateController : ISubject
    {
        private readonly List<IObserver> observers;

        private readonly AccommodationDateHandler _startingDateHandler;

        private List<AccommodationDate> _dates;

        public AccommodationDateController()
        {
            _startingDateHandler = new AccommodationDateHandler();
            _dates = new List<AccommodationDate>();
            Load();
        }

        public void Load()
        {
            _dates = _startingDateHandler.Load();
        }

        public List<AccommodationDate> GetAll()
        {
            return _dates;
        }

        public AccommodationDate GetByID(int id)
        {
            return _dates.Find(date => date.Id == id);
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

