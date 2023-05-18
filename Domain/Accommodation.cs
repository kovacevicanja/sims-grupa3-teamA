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
        public string AccommodationName { get; set; }
        public Location Location { get; set; }
        public int IdLocation { get; set; }
        public AccommodationType Type { get; set; }
        public int MaxGuestNumber { get; set; }
        public int MinDays { get; set; }
        public int CancellationPeriod { get; set; }
        public bool IsRecentlyRenovated { get; set; }
        public List<AccommodationImage> Images { get; set; }
        public User Owner { get; set; }

        public Accommodation() {
            Images = new List<AccommodationImage>();
            Owner = new User();
        }
        public Accommodation(int id, string name, int idLocation, Location location, AccommodationType type, int maxGuestNumber, int minDays, int cancellationPeriod, List<AccommodationImage> images)
        {
            Id = id;
            AccommodationName = name;
            IdLocation = idLocation;
            Location = location;
            Type = type;
            MaxGuestNumber = maxGuestNumber;
            MinDays = minDays;
            CancellationPeriod = cancellationPeriod;
            Images = images;
        }

  

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationName = values[1];
            IdLocation = int.Parse(values[2]);
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
            CancellationPeriod= int.Parse(values[6]);
            Owner.Id = int.Parse(values[7]);
            IsRecentlyRenovated = bool.Parse(values[8]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationName,
                IdLocation.ToString(),
                Type.ToString(),
                MaxGuestNumber.ToString(),
                MinDays.ToString(),
                CancellationPeriod.ToString(),
                Owner.Id.ToString(),
                IsRecentlyRenovated.ToString(),
            };
            return csvValues;
        }
    }
}
