using BookingProject.Domain;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IVoucherService
    {
        void DeleteExpiredVouchers();
        List<Voucher> GetUserVouhers(int guestId);
        void Create(Voucher voucher);
        List<Voucher> GetAll();
        Voucher GetById(int id);
        void Initialize();
        void Save(List<Voucher> vouchers);
        void SaveVouchers();
        void CreatePrizeVoucher(User guest);
        List<Voucher> GetAllGuestsVouchers(int guestId); 
    }
}