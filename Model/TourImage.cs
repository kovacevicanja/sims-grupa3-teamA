using BookingProject.ConversionHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Model
{
    internal class TourImage
    {
        public int Id { get; set; }

        public int TourId { get; set; } 
        public string Url { get; set; }

        public TourImage() { }  

        public TourImage(int id, string url, int tourId)
        {
            Id = id;
            Url = url;
            TourId = tourId;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            Url = values[2];

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                Url,
            };
            return csvValues;
        }


    }
}
