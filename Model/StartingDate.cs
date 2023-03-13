using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BookingProject.ConversionHelp;

namespace BookingProject.Model
{
    public class StartingDate : ISerializable
    {
        public int Id { get; set; }

        public int TourId { get; set; } 
        public DateTime StartingTime { get; set; }
        
        public StartingDate() { }

        public StartingDate(int id, int tourId,  DateTime startingTime)
        {
            Id = id;
            TourId = tourId;
            StartingTime = startingTime;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            StartingTime = DateConversion.StringToDate(values[2]);


        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                DateConversion.DateToString(StartingTime),
            };
            return csvValues;
        }




    }
}
