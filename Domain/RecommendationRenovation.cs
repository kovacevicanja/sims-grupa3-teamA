using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Domain
{
    public class RecommendationRenovation : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation AccommodationReservation { get; set; }
        public String Description { get; set; }
        public int UrgencyLevel { get; set; }

        public RecommendationRenovation()
        {
            AccommodationReservation = new AccommodationReservation();
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationReservation.Id = int.Parse(values[1]);
            Description = values[2];
            UrgencyLevel = int.Parse(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationReservation.Id.ToString(),
                Description,
                UrgencyLevel.ToString()
            };
            return csvValues;
        }
    }
}
