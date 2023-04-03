using BookingProject.ConversionHelp;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Model
{
    public class TourGuest : ISerializable
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int TourInstanceId { get; set; }

        public int KeyPointId { get; set; }

        public TourGuest() { }

        public TourGuest(int id, string name, int tourInstanceId, int keyPointId)
        {
            Id = id;
            Name = name;
            TourInstanceId = tourInstanceId;
            KeyPointId = keyPointId;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            TourInstanceId = int.Parse(values[2]);
            KeyPointId = int.Parse(values[3]);


        }

        public string[] ToCSV()
        {

            string[] csvValues =
        {
            Id.ToString(),
            Name,
            TourInstanceId.ToString(),
            KeyPointId.ToString()
        };
            return csvValues;

        }
    }
}
