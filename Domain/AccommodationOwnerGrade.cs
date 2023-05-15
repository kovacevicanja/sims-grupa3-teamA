using BookingProject.Model.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Serializer;
using BookingProject.Domain.Images;

namespace BookingProject.Model
{
    public class AccommodationOwnerGrade : ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; }
        public int Cleanliness { get; set; }
        public int OwnerCorectness { get; set; }
        public string AdditionalComment { get; set; }
        public string Reccommendation { get; set; }
        public User User { get; set; }
        public List<AccommodationGuestImage> guestImages;
        public AccommodationReservation AccommodationReservation { get; set; }

        public AccommodationOwnerGrade()
        {
            Accommodation = new Accommodation();
            guestImages = new List<AccommodationGuestImage>();
            User = new User();
            AccommodationReservation = new AccommodationReservation();

        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Accommodation.Id = int.Parse((string)values[1]);
            Cleanliness = int.Parse((string)values[2]);
            OwnerCorectness = int.Parse((string)values[3]);
            AdditionalComment = values[4];
            Reccommendation = values[5];
            User.Id = int.Parse((string)values[6]);
            AccommodationReservation.Id = int.Parse((string)values[7]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Accommodation.Id.ToString(),
                Cleanliness.ToString(),
                OwnerCorectness.ToString(),
                AdditionalComment,
                Reccommendation,
                User.Id.ToString(),
                AccommodationReservation.Id.ToString()
            };
            return csvValues;
        }
    }
}
