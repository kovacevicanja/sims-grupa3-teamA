using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Serializer;
using BookingProject.Serializer;

namespace BookingProject.Model
{
    internal class Location : ISerializable
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public Location() { }
        public Location(string country, string city)
        {
            Country = country;
            City = city;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Country = values[1];
            City = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Country,
                City,
            };
            return csvValues;
        }
    }
}
