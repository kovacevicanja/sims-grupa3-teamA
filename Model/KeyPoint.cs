using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Model
{
    public class KeyPoint : ISerializable
    {
        public int Id { get; set; }

        public int TourId { get; set; }
        public string Point { get; set; }
        public KeyPoint() 
        {
            TourId = -1;
        }

        public KeyPoint(int id, int tourId, string point)
        {
            Id = id;
            TourId = tourId;
            Point = point;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            Point = values[2];

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                Point,
            };
            return csvValues;

        }
    }
}
