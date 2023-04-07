using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class VoucherHandler
    {
        private const string FilePath = "../../Resources/Data/vouchers.csv";

        private readonly Serializer<Voucher> _serializer;

        public List<Voucher> _vouchers;

        public VoucherHandler()
        {
            _serializer = new Serializer<Voucher>();
            _vouchers = Load();
        }

        public List<Voucher> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Voucher> vouchers)
        {
            _serializer.ToCSV(FilePath, vouchers);
        }
    }
}
