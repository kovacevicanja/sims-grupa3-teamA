using BookingProject.ConversionHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using BookingProject.Serializer;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Model
{
    public class AccommodationReservation : ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DaysToStay { get; set; }
        public User Guest { get; set; }

        public AccommodationReservation()
        {
            Accommodation = new Accommodation();
            Guest = new User();
        }
        public AccommodationReservation(int id, Accommodation ac, DateTime iDate, DateTime endD, int dts)
        {
            Id = id;
            Accommodation = ac;
            InitialDate = iDate;
            EndDate = endD;
            DaysToStay = dts;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Accommodation.Id = int.Parse(values[1]);
            InitialDate = DateConversion.StringToDateAccommodation(values[2]);
            EndDate = DateConversion.StringToDateAccommodation(values[3]);
            DaysToStay = int.Parse(values[4]);
            Guest.Id = int.Parse(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Accommodation.Id.ToString(),
                DateConversion.DateToStringAccommodation(InitialDate),
                DateConversion.DateToStringAccommodation(EndDate),
                DaysToStay.ToString(),
                Guest.Id.ToString()
            };
            return csvValues;
        }
            
        }
    }


