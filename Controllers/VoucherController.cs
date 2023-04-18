using BookingProject.Controller;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Domain.Enums;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Serializer;
using BookingProject.Services.Interfaces;
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
        private readonly IVoucherService _voucherService;
        public VoucherController()
        {
            _voucherService = Injector.CreateInstance<IVoucherService>();
        }
        public void DeleteExpiredVouchers()
        {
            _voucherService.DeleteExpiredVouchers();
        }
        public List<Voucher> GetUserVouhers(int guestId)
        {
            return _voucherService.GetUserVouhers(guestId);
        }
        public void Create(Voucher voucher)
        {
            _voucherService.Create(voucher);
        }
        public List<Voucher> GetAll()
        {
            return _voucherService.GetAll();    
        }
        public Voucher GetByID(int id)
        {
            return _voucherService.GetByID(id);
        }

        public void Save(List<Voucher> _vouchers)
        {
            _voucherService.Save(_vouchers);
        }
    }
}