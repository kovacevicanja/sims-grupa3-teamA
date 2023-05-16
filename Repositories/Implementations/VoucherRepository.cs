using BookingProject.Controller;
using BookingProject.Domain;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private const string FilePath = "../../Resources/Data/vouchers.csv";
        private Serializer<Voucher> _serializer;
        public List<Voucher> _vouchers;

        public VoucherRepository()
        {
            _serializer = new Serializer<Voucher>();
            _vouchers = Load();
        }
        public void Initialize() { }
        public List<Voucher> Load()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save(List<Voucher> vouchers)
        {
            _serializer.ToCSV(FilePath, vouchers);
        }
        public void SaveVouchers()
        {
            _serializer.ToCSV(FilePath, _vouchers);
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
            Save(_vouchers);
        }
        public List<Voucher> GetAll()
        {
            return _vouchers.ToList();
        }
        public Voucher GetById(int id)
        {
            return _vouchers.Find(voucher => voucher.Id == id);
        }
    }
}