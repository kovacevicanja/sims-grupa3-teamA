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
    public class VoucherController: ISubject
    {
        private readonly List<IObserver> observers;

        private readonly VoucherHandler _voucherHandler;

        private List<Voucher> _vouchers;

        public VoucherController()
        {
            _voucherHandler = new VoucherHandler();
            _vouchers = new List<Voucher>();
            Load();
        }

        public void Load()
        {
            _vouchers = _voucherHandler.Load();
        }

        public List<Voucher> GetAll()
        {
            return _vouchers;
        }

        public void Create(Voucher voucher)
        {
            voucher.Id = GenerateId();
            _vouchers.Add(voucher);
        }

        public void SaveVoucher()
        {
            _voucherHandler.Save(_vouchers);
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
