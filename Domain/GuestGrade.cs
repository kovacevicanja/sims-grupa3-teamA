using BookingProject.Model.Enums;
using BookingProject.Model.Images;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Model
{
    public class GuestGrade: ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation AccommodationReservation { get; set; }
        public int Cleanliness { get; set; }
        public int Communication { get; set; }
        public int ObservanceOfRules { get; set; }
        public int Decency { get; set; }
        public int Noisiness { get; set; }
        public string Comment { get; set; }

        public GuestGrade()
        {
            AccommodationReservation = new AccommodationReservation();
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationReservation.Id = int.Parse((string)values[1]);
            Cleanliness = int.Parse((string)values[2]);
            Communication = int.Parse((string)values[3]);
            ObservanceOfRules = int.Parse((string)values[4]);
            Decency = int.Parse((string)values[5]);
            Noisiness = int.Parse((string)values[6]);
            Comment = values[7];
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationReservation.Id.ToString(),
                Cleanliness.ToString(),
                Communication.ToString(),
                ObservanceOfRules.ToString(),
                Decency.ToString(),
                Noisiness.ToString(),
                Comment,
            };
            return csvValues;
        }
    }
}
