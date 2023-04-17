using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Domain;
using BookingProject.Serializer;

namespace BookingProject.Model.Images
{
    public class TourImage : ISerializable
    {
        public int Id { get; set; }
        public Tour Tour { get; set; }
        public string Url { get; set; }

        public TourImage() {
            Tour = new Tour();
            Tour.Id = -1;
        }

        public TourImage(int id, string url, Tour tour)
        {
            Id = id;
            Url = url;
            Tour = tour;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Tour.Id = int.Parse(values[1]);
            Url = values[2];

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Tour.Id.ToString(),
                Url,
            };
            return csvValues;
        }


    }
}
