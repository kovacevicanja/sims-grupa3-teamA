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
            observers = new List<IObserver>();
            Load();
        }

        public void Load()
        {
            _dates = _startingDateHandler.Load();
        }

        private int GenerateId()
        {
            int maxId = 0;
            foreach (TourDateTime date in _dates)
            {
                if (date.Id > maxId)
                {
                    maxId = date.Id;
                }
            }
            return maxId + 1;
        }

        public void Create(TourDateTime date)
        {
            date.Id= GenerateId();
            _dates.Add(date);
            NotifyObservers();
        }

        public void Save()
        {
            _startingDateHandler.Save(_dates);
            NotifyObservers();
        }


        public void LinkToTour(int id)
        {

            foreach (TourDateTime startingDate in _dates)
            {
                if (startingDate.TourId == -1)
                {
                    startingDate.TourId = id;
                }

            }
            NotifyObservers();
        }

        public void CleanUnused()
        {
            _dates.RemoveAll (d => d.TourId == -1);
            NotifyObservers();

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

