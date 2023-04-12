using BookingProject.Controller;
using BookingProject.Domain;
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

        private ToursGuestsController _toursGuestsController;

        private TourReservationController _tourReservationController;   
        private VoucherController _voucherController;

        public Guest2Controller()
        {
            _guest2Handler = new Guest2Handler();
            _guests2 = new List<Guest2>();
            observers = new List<IObserver>();
            //_voucherController = new VoucherController();
            Load();
        }

        public void Load()
        {
            _guests2 = _guest2Handler.Load();
            GuestMyVouchersBind();
            GuestTourReservationBind();
        }
        public void GuestMyVouchersBind()
        {
            List<Voucher> vouchers = new List<Voucher>();
            VoucherHandler voucherHandler = new VoucherHandler();
            vouchers = voucherHandler.Load();

            foreach (Guest2 guest in _guests2)
            {
                foreach (Voucher voucher in vouchers)
                {
                    if (guest.Id == voucher.Guest.Id)
                    {
                        guest.MyVouchers.Add(voucher);
                    }
                }
            }
        }
        public void GuestTourReservationBind()
        {
            List <TourReservation> tourReservations = new List<TourReservation>();
            TourReservationHandler tourReservationHandler = new TourReservationHandler();
            tourReservations = tourReservationHandler.Load();

            foreach (Guest2 guest in _guests2)
            {
                foreach (TourReservation tourReservation in tourReservations)
                {
                    if (guest.Id == tourReservation.Guest.Id)
                    {
                        guest.MyTours.Add(tourReservation);
                    }
                }
            }
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
            //return _guests2.Find(guest2 => guest2.Id == id);
            foreach (Guest2 guest2 in _guests2)
            {
                if (guest2.Id == id)
                {
                    return guest2;
                }
            }
            return null;
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
