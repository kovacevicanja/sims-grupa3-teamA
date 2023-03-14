using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingProject.FileHandler;
using BookingProject.Model;
using OisisiProjekat.Observer;

namespace BookingProject.Controller
{
    internal class TourReservationController : ISubject
    {
        private readonly List<IObserver> observers;

        private readonly TourReservationHandler _reservationHandler;

        private List<TourReservation> _reservations;

        private TourReservation tourRes { get; set;  }

        public TourReservationController()
        {
            _reservationHandler = new TourReservationHandler();
            _reservations = new List<TourReservation>();
            Load();
        }

        public void Load()
        {
            _reservations = _reservationHandler.Load();
        }

        public List<TourReservation> GetAll()
        {
            return _reservations;
        }

        public TourReservation GetByID(int id)
        {
            return _reservations.Find(date => date.Id == id);
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

        public ObservableCollection<TourReservation> TryToBook(ObservableCollection<TourReservation> tourReservation, Tour choosenTour, string numberOfGuest)
        {
            if (int.Parse(numberOfGuest) <= choosenTour.MaxGuests)
            {
                choosenTour.MaxGuests = choosenTour.MaxGuests - int.Parse(numberOfGuest);
                tourRes.TourId = choosenTour.Id;
                tourRes.GuestsNumberPerReservation = choosenTour.MaxGuests;
                tourReservation.Add(tourRes);
            }
            else
            {
                MessageBox.Show("You have exceeded the maximum number of guests per tour!");
                //dalje ponudi mu neke slicne ture
            }

            return tourReservation;
        }
    }
}
