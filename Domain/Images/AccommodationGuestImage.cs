using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Domain.Images
{
    public class AccommodationGuestImage : ISerializable
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public AccommodationOwnerGrade Grade { get; set; }
        public User Guest { get; set; }
        public AccommodationGuestImage()
        {
            Grade = new AccommodationOwnerGrade();
            Guest = new User();
        }


        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Url = values[1];
            Grade.Id = int.Parse(values[2]);
            Guest.Id = int.Parse(values[3]);

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Url,
                Grade.Id.ToString(),
                Guest.Id.ToString()
            };
            return csvValues;
        }


    }
}
