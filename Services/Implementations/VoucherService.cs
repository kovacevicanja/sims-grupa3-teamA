using BookingProject.Domain;
using BookingProject.Repository;
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
        private List<Voucher> _vouchers;
        private VoucherRepository _voucherRepository;
        public VoucherService() 
        {
            _vouchers = new List<Voucher>();
            _voucherRepository = new VoucherRepository();
            Load();
        }
        
        public void Load()
        {
            _vouchers = _voucherRepository.Load();
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
            _voucherRepository.Save();
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
    }
}