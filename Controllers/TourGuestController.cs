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
    public class TourGuestController
    {
            private readonly List<IObserver> observers;

            private readonly TourGuestHandler _tourGuestHandler;

            private List<TourGuest> _tourGuests;

            public TourGuestController()
            {
                _tourGuestHandler = new TourGuestHandler();
                _tourGuests = new List<TourGuest>();
                observers = new List<IObserver>();
                Load();
            }

            public void Load()
            {
                _tourGuests = _tourGuestHandler.Load(); 
            }

            private int GenerateId()
            {
                int maxId = 0;
                foreach (TourGuest guest in _tourGuests)
                {
                    if (guest.Id > maxId)
                    {
                        maxId = guest.Id;
                    }
                }
                return maxId + 1;
            }

            public void Create(TourGuest guest)
            {
                guest.Id = GenerateId();
                _tourGuests.Add(guest);
                NotifyObservers();
            }

            public void Save()
            {
                _tourGuestHandler.Save(_tourGuests);
                NotifyObservers();
            }


            public List<TourGuest> GetAll()
            {
                return _tourGuests;
            }

            public TourGuest GetByID(int id)
            {
                return _tourGuests.Find(guest => guest.Id == id);
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
