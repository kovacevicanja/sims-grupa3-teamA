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
    public class StartingDateController : ISubject
    {
        private readonly List<IObserver> observers;

        private readonly StartingDateHandler _startingDateHandler;

        private List<StartingDate> _dates;

        public StartingDateController()
        {
            _startingDateHandler = new StartingDateHandler();
            _dates = new List<StartingDate>();
            Load();
        }

        public void Load()
        {
            _dates = _startingDateHandler.Load();
        }

        public List<StartingDate> GetAll()
        {
            return _dates;
        }

        public StartingDate GetByID(int id)
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

