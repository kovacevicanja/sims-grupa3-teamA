using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BookingProject.Serializer;
using BookingProject.Model.Enums;
using BookingProject.Model.Images;

namespace BookingProject.Model
{
    public class Accommodation : ISerializable
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public AccommodationType Type { get; set; }
        public int MaxGuestNumber { get; set; }
        public int MinDays { get; set; }
        public int CancelationPeriod { get; set; }
        public List<AccommodationImages> Images { get; set; }

        public Accommodation() {
            Location = new Location();
            Images = new List<AccommodationImages>();
        }
        public Accommodation(int id, string name, Location location, AccommodationType type, int maxGuestNumber, int minDays, int cancelationPeriod)
        {
            Id = id;
            Name = name;
            Location = location;
            Type = type;
            MaxGuestNumber = maxGuestNumber;
            MinDays = minDays;
            CancelationPeriod = cancelationPeriod;
        }

  

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Location.Id = int.Parse(values[2]);
            AccommodationType accommodationType;
            if (Enum.TryParse<AccommodationType>(values[3], out accommodationType))
            {
                Type = accommodationType;
            } else
            {
                Type = AccommodationType.APARTMENT;
                System.Console.WriteLine("Doslo je do greske prilikom ucitavanja tipa smestaja");
            }
            MaxGuestNumber = int.Parse(values[4]);
            MinDays= int.Parse(values[5]);
            CancelationPeriod= int.Parse(values[6]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                Location.Id.ToString(),
                Type.ToString(),
                MaxGuestNumber.ToString(),
                MinDays.ToString(),
                CancelationPeriod.ToString(),
            };
            return csvValues;
        }
    }
}
