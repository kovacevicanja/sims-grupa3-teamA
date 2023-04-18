using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Repositories;
using BookingProject.Repositories.Intefaces;
using BookingProject.Repository;
using BookingProject.Serializer;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services
{
    public class VoucherService : IVoucherService
    {
        private IVoucherRepository _voucherRepository;
        public VoucherService() { }
        public void Initialize()
        {
            _voucherRepository = Injector.CreateInstance<IVoucherRepository>();
        }
        public void DeleteExpiredVouchers()
        {
            List<Voucher> copyList = new List<Voucher>(_voucherRepository.GetAll());
            foreach (Voucher voucher in copyList)
            {
                if (voucher.EndDate <= DateTime.Now)
                {
                    _voucherRepository.GetAll().Remove(voucher);
                }
            }
            //_voucherRepository.Save();
        }
        public List<Voucher> GetUserVouhers(int guestId)
        {
            List<Voucher> guestsVouchers = new List<Voucher>();
            foreach (Voucher voucher in _voucherRepository.GetAll())
            {
                if (voucher.Guest.Id == guestId && voucher.State.ToString() != "USED")
                {
                    guestsVouchers.Add(voucher);
                }
            }
            return guestsVouchers;
        }

        public void Create(Voucher voucher)
        {
            _voucherRepository.Create(voucher);
        }

        public List<Voucher> GetAll()
        {
            return _voucherRepository.GetAll();
        }

        public Voucher GetByID(int id)
        {
            return _voucherRepository.GetByID(id);
        }

        public void Save(List<Voucher> vouchers)
        {
            _voucherRepository.Save(vouchers);
        }
    }
}