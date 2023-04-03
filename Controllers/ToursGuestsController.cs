using BookingProject.Controller;
using BookingProject.Domain;
using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.View;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controllers
{
    public class ToursGuestsController
    {
        private readonly List<IObserver> observers;
        private readonly ToursGuestsHandler _toursGuestsHandler;
        private List<ToursGuests> _toursGuests;
        public TourController TourController { get; set; }
        public Guest2Controller GuestController { get; set; }
        public ToursGuestsController()
        {
            _toursGuestsHandler = new ToursGuestsHandler();
            _toursGuests = new List<ToursGuests>();
            observers = new List<IObserver>();
            Load();
        }
        public void Load()
        {
            _toursGuests = _toursGuestsHandler.Load();
        }
        private int GenerateId()
        {
            int maxId = 0;
            foreach (ToursGuests toursGuests in _toursGuests)
            {
                if (toursGuests.Id > maxId)
                {
                    maxId = toursGuests.Id;
                }
            }
            return maxId + 1;
        }
        public void Create(ToursGuests toursGuests)
        {
            toursGuests.Id = GenerateId();
            _toursGuests.Add(toursGuests);
            NotifyObservers();
        }
        public void Save()
        {
            _toursGuestsHandler.Save(_toursGuests);
            NotifyObservers();
        }
        public List<ToursGuests> GetAll()
        {
            return _toursGuests;
        }
        public ToursGuests GetByID(int id)
        {
            return _toursGuests.Find(toursGuests => toursGuests.Id == id);
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

        //bind Tours and Guests

        public void BindToursAndGuests()
        {
            foreach (ToursGuests toursGuests in _toursGuests)
            {
                Tour tour = TourController.GetByID(toursGuests.Tour.Id);
                Guest2 guest = GuestController.GetByID(toursGuests.Guest.Id);
                if (tour == null || guest == null)
                {
                    System.Console.WriteLine("Error when loading the connection between the tour and the guest");
                }
                else
                {
                    tour.TourGuests.Add(guest);
                    guest.MyTours.Add(tour);   
                }
            }
        }
    }
}
