using BookingProject.Controller;
using BookingProject.Domain;
using BookingProject.Domain.Enums;
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
    public class VoucherController
    {
        private readonly List<IObserver> observers;
        private readonly VoucherHandler _voucherHandler;
        private List<Voucher> _vouchers;
        private TourReservationController _tourReservationController { get; set; }
        public VoucherController()
        {
            _voucherHandler = new VoucherHandler();
            _vouchers = new List<Voucher>();
            observers = new List<IObserver>();
            Load();
        }
        public void Load()
        {
            _vouchers = _voucherHandler.Load();
        }
        public int GenerateId()
        {
            int maxId = 0;
            foreach (Voucher voucher in _vouchers)
            {
                if (voucher.Id > maxId)
                {
                    maxId = voucher.Id;
                }
            }
            return maxId + 1;
        }
        public void Create(Voucher voucher)
        {
            voucher.Id = GenerateId();
            _vouchers.Add(voucher);
            NotifyObservers();
        }
        public void Save()
        {
            _voucherHandler.Save(_vouchers);
            NotifyObservers();
        }
        public List<Voucher> GetAll()
        {
            return _vouchers;
        }
        public void DeleteExpiredVouchers()
        {
            List<Voucher> copyList = new List<Voucher>(_vouchers);
            foreach (Voucher voucher in copyList)
            {
                if (voucher.EndDate <= DateTime.Now)
                {
                    _vouchers.Remove(voucher);
                }
            }
            Save();
        }
        public List<Voucher> GetUserVouhers(int guestId)
        {
            List<Voucher> guestsVouchers = new List<Voucher>();
            foreach (Voucher voucher in _vouchers)
            {
                if (voucher.Guest.Id == guestId && voucher.State.ToString() != "USED")
                {
                    guestsVouchers.Add(voucher);
                }
            }
            return guestsVouchers;
        }
        public Voucher GetByID(int id)
        {
            return _vouchers.Find(voucher => voucher.Id == id);
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
