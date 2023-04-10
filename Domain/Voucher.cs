using BookingProject.Model.Enums;
using BookingProject.Model;
using BookingProject.Serializer;
using ISerializable = BookingProject.Serializer.ISerializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Domain.Enums;
using BookingProject.ConversionHelp;
using System.Runtime.Serialization;

namespace BookingProject.Domain
{
    public class Voucher : ISerializable
    {
        public int Id { get; set; }
        public Guest2 Guest { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public VoucherState State { get; set; }

        public Voucher()
        {
            State = VoucherState.CREATED;
            Guest = new Guest2();
        }

        public Voucher(int id, Guest2 guest, DateTime startDate, DateTime endDate, VoucherState state)
        {
            Id = id;
            Guest = guest;
            StartDate = startDate;
            EndDate = endDate;
            State = state;
        }

        public void FromCSV(string[] values)
        {


            Id = int.Parse(values[0]);
            Guest.Id = int.Parse(values[1]);
            StartDate = DateConversion.StringToDateTour(values[2]);
            EndDate = DateConversion.StringToDateTour(values[3]);


            VoucherState voucherState;
            if (Enum.TryParse<VoucherState>(values[4], out voucherState))
            {
                State = voucherState;
            }
            else
            {
                voucherState = VoucherState.CREATED;
                System.Console.WriteLine("An error occurred while loading the state!");
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Guest.Id.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                State.ToString(),

            };
            return csvValues;
        }

    }
}

