using BookingProject.ConversionHelp;
using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Domain
{
    public class AccommodationRenovation : ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Description { get; set; }
        public AccommodationRenovation() {
            Accommodation = new Accommodation();
        }

        public AccommodationRenovation(int accommodationId, DateTime startDate, DateTime endDate, String description)
        {
            Accommodation = new Accommodation();
            Accommodation.Id = accommodationId;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Accommodation.Id.ToString(),
                DateConversion.DateToStringAccommodation(StartDate),
                DateConversion.DateToStringAccommodation(EndDate),

                Description,
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation.Id = Convert.ToInt32(values[1]);
            StartDate = DateConversion.StringToDateAccommodation(values[3]);
            EndDate = DateConversion.StringToDateAccommodation(values[4]);
            Description = values[5];
        }

    }
}
