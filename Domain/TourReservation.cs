using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.ConversionHelp;
using BookingProject.Serializer;

namespace BookingProject.Model
{
    internal class TourReservation : ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int GuestsNumberPerReservation { get; set;  }
        public DateTime ReservationStartingTime { get; set; }
        public TourReservation() { }
        public TourReservation(int id, int tourId, int guestsNumberPerReservation, DateTime reservationStartingTime)
        {
            Id = id;
            TourId = tourId;
            GuestsNumberPerReservation = guestsNumberPerReservation;
            ReservationStartingTime = reservationStartingTime;
        }

        public void FromCSV(string[] values)
        { 
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            GuestsNumberPerReservation = int.Parse(values[2]);
            ReservationStartingTime = DateConversion.StringToDateTour(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                GuestsNumberPerReservation.ToString(),
                DateConversion.DateToStringTour(ReservationStartingTime)
            };
            return csvValues;
        }

    }
}