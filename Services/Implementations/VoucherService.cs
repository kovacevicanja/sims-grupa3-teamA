using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
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
            List<Voucher> vouchers = new List<Voucher>(_voucherRepository.GetAll());
            List<Voucher> copyList = new List<Voucher>(_voucherRepository.GetAll());
            foreach (Voucher voucher in copyList)
            {
                if (voucher.EndDate <= DateTime.Now)
                {
                    vouchers.Remove(voucher);
                    _voucherRepository.Save(vouchers);
                }
            }
        }
        public List<Voucher> GetUserVouhers(int guestId)
        {
            List<Voucher> guestsVouchers = new List<Voucher>();
            foreach (Voucher voucher in _voucherRepository.GetAll())
            {
                if (voucher.Guest.Id == guestId && voucher.State.ToString() != "USED" && voucher.EndDate > DateTime.Now)
                {
                    guestsVouchers.Add(voucher);
                }
            }
            return guestsVouchers;
        }

        public List<Voucher> GetAllGuestsVouchers(int guestId)
        {
            List<Voucher> guestsVouchers = new List<Voucher>();
            foreach (Voucher voucher in _voucherRepository.GetAll())
            {
                if (voucher.Guest.Id == guestId)
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
        public Voucher GetById(int id)
        {
            return _voucherRepository.GetById(id);
        }
        public void Save(List<Voucher> vouchers)
        {
            _voucherRepository.Save(vouchers);
        }
        public void SaveVouchers()
        {
            _voucherRepository.SaveVouchers();
        }
        public void CreatePrizeVoucher(User guest)
        {
            _voucherRepository.CreatePrizeVoucher(guest);
        }
    }
}