using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingProject.Domain
{
    public class ToursGuests : ISerializable
    {
        public int Id { get; set; }
        public Tour Tour { get; set; }  
        public Guest2 Guest { get; set; }
        public ToursGuests () { }
        public ToursGuests (int id, Tour tour, Guest2 guest)
        {
            Id = id;
            Tour = tour;
            Guest = guest;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Tour.Id = int.Parse(values[1]);
            Guest.Id = int.Parse(values[2]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Tour.Id.ToString(),
                Guest.Id.ToString()
            };
            return csvValues;
        }
    }
}
