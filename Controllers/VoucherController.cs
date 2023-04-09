using BookingProject.Controller;
using BookingProject.Domain;
using BookingProject.FileHandler;
using BookingProject.Model;
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



        private int GenerateId()
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
