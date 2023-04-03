using BookingProject.Controller;
using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.Model.Images;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controllers
{
    public class Guest2Controller : ISubject
    {
        private readonly List<IObserver> observers;

        private readonly Guest2Handler _guest2Handler;

        private List<Guest2> _guests2;

        public Guest2Controller()
        {
            _guest2Handler = new Guest2Handler();
            _guests2 = new List<Guest2>();
            observers = new List<IObserver>();
            Load();
        }

        public void Load()
        {
            _guests2 = _guest2Handler.Load();
        }

        private int GenerateId()
        {
            int maxId = 0;
            foreach (Guest2 guest2 in _guests2)
            {
                if (guest2.Id > maxId)
                {
                    maxId = guest2.Id;
                }
            }
            return maxId + 1;
        }

        public void Create(Guest2 guest2)
        {
            guest2.Id = GenerateId();
            _guests2.Add(guest2);
            NotifyObservers();
        }

        public void Save()
        {
            _guest2Handler.Save(_guests2);
            NotifyObservers();
        }

        public List<Guest2> GetAll()
        {
            return _guests2;
        }

        public Guest2 GetByID(int id)
        {
            return _guests2.Find(guest2 => guest2.Id == id);
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
