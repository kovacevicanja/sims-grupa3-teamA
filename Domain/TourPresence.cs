using BookingProject.Model.Enums;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Domain
{
    public class TourPresence : ISerializable
    {
        public int Id { get; set; }

        public int TourId { get; set; }
        public int UserId { get; set; }
        public int KeyPointId { get; set; }

        public TourPresence()
        {
            KeyPointId = -1;
        }

        public TourPresence(int id, int tourId, int userId, int keyPointId)
        {
            Id = id;
            TourId = tourId;
            UserId = userId;
            KeyPointId = keyPointId;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            UserId = int.Parse(values[2]);
            KeyPointId = int.Parse(values[3]);

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                UserId.ToString(),
                KeyPointId.ToString(),
            };
            return csvValues;

        }
    }
}
