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
    public class TourStartingTimeController : ISubject
    {
        private readonly List<IObserver> observers;

        private readonly TourStartingTimeHandler _startingDateHandler;

        private List<TourDateTime> _dates;

        public TourStartingTimeController()
        {
            _startingDateHandler = new TourStartingTimeHandler();
            _dates = new List<TourDateTime>();
            Load();
        }

        public void Load()
        {
            _dates = _startingDateHandler.Load();
        }

        public List<TourDateTime> GetAll()
        {
            return _dates;
        }

        public TourDateTime GetByID(int id)
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

