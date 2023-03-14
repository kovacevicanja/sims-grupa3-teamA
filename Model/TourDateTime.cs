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
    public class TourDateTime : ISerializable
    {
        public int Id { get; set; }

        public int TourId { get; set; } 
        public DateTime StartingDateTime { get; set; }
        
        public TourDateTime() { }

        public TourDateTime(int id, int tourId,  DateTime startingDateTime)
        {
            Id = id;
            TourId = tourId;
            StartingDateTime = startingDateTime;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            StartingDateTime = DateConversion.StringToDateTour(values[2]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                DateConversion.DateToStringTour(StartingDateTime),
            };
            return csvValues;
        }
    }
}
