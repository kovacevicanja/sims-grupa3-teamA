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

        private int GenerateId()
        {
            int maxId = 0;
            foreach (StartingDate date in _dates)
            {
                if (date.Id > maxId)
                {
                    maxId = date.Id;
                }
            }
            return maxId + 1;
        }

        public void Create(StartingDate date)
        {
            date.Id= GenerateId();
            _dates.Add(date);
        }

        public void Save()
        {
            _startingDateHandler.Save(_dates);
        }


        public void LinkToTour(int id)
        {

            foreach (StartingDate startingDate in _dates)
            {
                if (startingDate.TourId == -1)
                {
                    startingDate.TourId = id;
                }

            }
        }

        public void CleanUnused()
        {
            _dates.RemoveAll (d => d.TourId == -1);

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

