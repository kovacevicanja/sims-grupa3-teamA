using BookingProject.Model.Enums;
using BookingProject.Model.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Serializer;

namespace BookingProject.Model
{
    public class TourTimeInstance : ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; } 

        public int DateId { get; set; }
        public TourState State { get; set; }     
        public Tour Tour { get; set; }
        public TourDateTime TourTime { get; set; }

        public TourTimeInstance()
        {
            State = TourState.CREATED;
        }

        public TourTimeInstance(int id, int tourId, TourState state, int dateId)
        {
            Id = id;
            TourId = tourId;
            State = state;
            DateId = dateId;
        }

        public void FromCSV(string[] values)
        {


            Id = int.Parse(values[0]);
            TourId= int.Parse(values[1]);
            DateId= int.Parse(values[2]);

            TourState tourState;
            if (Enum.TryParse<TourState>(values[3], out tourState))
            {
                State = tourState;
            }
            else
            {
                tourState = TourState.STARTED;
                System.Console.WriteLine("Doslo je do greske prilikom ucitavanja stanja!");

            }
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                DateId.ToString(),
                State.ToString(),

            };
            return csvValues;
        }

    }
}
