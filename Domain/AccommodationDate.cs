using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.ConversionHelp;
using BookingProject.Serializer;

namespace BookingProject.Model
{
    public class AccommodationDate : ISerializable
    {
        public int Id { get; set; }

        public int AccommodationId { get; set; }
        public DateTime StartingDate { get; set; }

        public AccommodationDate() { }

        public AccommodationDate(int id, int accommodationId, DateTime startingDate)
        {
            Id = id;
            AccommodationId = accommodationId;
            StartingDate = startingDate;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationId = int.Parse(values[1]);
            StartingDate = DateConversion.StringToDateAccommodation(values[2]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationId.ToString(),
                DateConversion.DateToStringAccommodation(StartingDate),
            };
            return csvValues;
        }
    }
}

