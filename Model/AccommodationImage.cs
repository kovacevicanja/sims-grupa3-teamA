using BookingProject.ConversionHelp;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Model
{
    public class AccommodationImage : ISerializable
    {
        public int Id { get; set; }

        public int AccommodationId { get; set; }
        public string Url { get; set; }

        public AccommodationImage() { }

        public AccommodationImage(int id, string url, int accommodationId)
        {
            Id = id;
            Url = url;
            AccommodationId = accommodationId;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationId = int.Parse(values[1]);
            Url = values[2];

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationId.ToString(),
                Url,
            };
            return csvValues;
        }


    }
}