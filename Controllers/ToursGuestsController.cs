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

        public TourReservationController TourReservationController { get; set; }
        public ToursGuestsController()
        {
            _toursGuestsHandler = new ToursGuestsHandler();
            _toursGuests = new List<ToursGuests>();
            observers = new List<IObserver>();
            TourController =  new TourController();
            GuestController = new Guest2Controller();  
            TourReservationController = new TourReservationController();
            Load();
        }
        public void Load()
        {
            _toursGuests = _toursGuestsHandler.Load();
            //BindToursAndGuests();
            //SetToursAndGuests();
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

        /*
        public void BindToursAndGuests()
        {
            TourReservation tour = new  TourReservation();
            Guest2 guest = new Guest2();
            foreach (ToursGuests toursGuests in _toursGuests)
            {
                tour = TourReservationController.GetByID(toursGuests.TourReservation.Tour.Id);
                guest = GuestController.GetByID(toursGuests.Guest.Id);
                if (tour == null || guest == null)
                {
                    System.Console.WriteLine("Error when loading the connection between the tour and the guest");
                }
                else
                {
                    tour.Guests.Add(guest);
                    guest.MyTours.Add(tour);
                }
            }
        }

        public void SetToursAndGuests()
        {
                TourReservationController.Load();
                foreach (ToursGuests tg in _toursGuests)
                {
                    TourReservation tour = TourReservationController.GetByID(tg.TourReservation.Id);
                    tg.TourReservation = tour;
                }

                GuestController.Load();
                foreach (ToursGuests tg in _toursGuests)
                {
                    Guest2 guest = GuestController.GetByID(tg.Guest.Id);
                    tg.Guest = guest;
                }
        }
        */
    }
}
