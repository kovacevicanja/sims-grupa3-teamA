using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.ConversionHelp;
using BookingProject.Serializer;

namespace BookingProject.Model
{
    public class TourReservation : ISerializable
    {
        public int Id { get; set; }
        public Tour Tour { get; set; }
        public int GuestsNumberPerReservation { get; set;  }
        public DateTime ReservationStartingTime { get; set; }
        //public List <Guest2> Guests { get; set; }
        public Guest2 Guest { get; set; }   

        //id gosta koji je rez turu 
        //prolaxim kroz rez
        //trazim taj id gosta u svakoj rez i svaku tu rez stavim u neku listu mytours
        //i onda samo to prikazem u wpfu
        //ili alternativa prodjem sve i samo zalepim taj komad za turu
        public TourReservation() 
        {
            Tour = new Tour();
            Guest = new Guest2();
        }
        public TourReservation(int id, Tour tour, int guestsNumberPerReservation, DateTime reservationStartingTime, Guest2 guest)
        {
            Id = id;
            Tour = tour;
            GuestsNumberPerReservation = guestsNumberPerReservation;
            ReservationStartingTime = reservationStartingTime;
            Guest = guest;
        }

        public void FromCSV(string[] values)
        { 
            Id = int.Parse(values[0]);
            Tour.Id = int.Parse(values[1]);
            GuestsNumberPerReservation = int.Parse(values[2]);
            ReservationStartingTime = DateConversion.StringToDateTour(values[3]);
            Guest.Id = int.Parse(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Tour.Id.ToString(),
                GuestsNumberPerReservation.ToString(),
                DateConversion.DateToStringTour(ReservationStartingTime),
                Guest.Id.ToString()
            };
            return csvValues;
        }

    }
}