using BookingProject.ConversionHelp;
using BookingProject.Domain.Enums;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Domain
{
    public class ReservationMovingRequest: ISerializable
    {
        public int Id { get; set;}
        public AccommodationReservation Reservation { get; set; }
        public String Comment { get; set; }
        public DateTime NewStart { get; set; }
        public DateTime NewEnd { get; set; }
        public RequestStatus Status { get; set; }

        public ReservationMovingRequest()
        {
            Reservation = new AccommodationReservation();
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Reservation.Id = int.Parse(values[1]);
            Comment = values[2];
            NewStart = DateConversion.StringToDateAccommodation(values[3]);
            NewEnd = DateConversion.StringToDateAccommodation(values[4]);
            RequestStatus status;
            if (Enum.TryParse<RequestStatus>(values[5], out status))
            {
                Status = status;
            }
            else
            {
                Status = RequestStatus.ON_WAIT;
                System.Console.WriteLine("Doslo je do greske prilikom ucitavanja tipa smestaja");
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Reservation.Id.ToString(),
                Comment,
                DateConversion.DateToStringAccommodation(NewStart),
                DateConversion.DateToStringAccommodation(NewEnd),
                Status.ToString(),
            };
            return csvValues;
        }
    }
}
