using BookingProject.Model.Enums;
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

        public KeyPointState State { get; set; }
        public KeyPoint() 
        {
            TourId = -1;
            State = KeyPointState.EMPTY;
        }

        

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            Point = values[2];
            KeyPointState keyPointState;
            if (Enum.TryParse<KeyPointState>(values[3], out keyPointState))
            {
                State = keyPointState;
            }
            else
            {
                keyPointState = KeyPointState.EMPTY;
                System.Console.WriteLine("Doslo je do greske prilikom ucitavanja stanja!");

            }

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                Point,
                State.ToString(),
            };
            return csvValues;

        }
    }
}
