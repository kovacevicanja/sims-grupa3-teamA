using System;
using System.Collections.Generic;
using System.Linq;
using BookingProject.Serializer;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Model.Images
{
    public class AccommodationImage : ISerializable
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int AccommodationId { get; set; }
        public AccommodationImage()
        {
            
        }

        public AccommodationImage(int id, string url, int accommodationId)
        {
            Id = id;
            Url = url;
            AccommodationId = accommodationId;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Url = values[1];
            AccommodationId = int.Parse(values[2]);

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Url,
                AccommodationId.ToString(),
            };
            return csvValues;
        }

       
    }
}
