using BookingProject.Domain;
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
        Voucher GetByID(int id);
    }
}