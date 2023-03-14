using BookingProject.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Model.Images;
using System.Xml.Linq;
using BookingProject.Serializer;

namespace BookingProject.Model
{
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }

        public Location Location { get; set; }
        public string Description { get; set; }
        public LanguageEnum Language { get; set; }
        public int MaxGuests { get; set; }
        public List<KeyPoint> KeyPoints { get; set; }
        public List<TourDateTime> StartingTime { get; set; }
        public double DurationInHours { get; set; }
        public List<TourImage> Images { get; set; }

        public Tour() { 
            KeyPoints = new List<KeyPoint>();
            StartingTime = new List<TourDateTime>();
            Images = new List<TourImage>();
           
        }

        public Tour(int id, string name, int locationId, Location location, string description, LanguageEnum language, int maxGuests, List<KeyPoint> keyPoints, List<TourDateTime> startingTime, double durationInHours, List<TourImage> images)
        {
            Id = id;
            Name = name;
            LocationId = locationId;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            DurationInHours = durationInHours;
        }

        public void FromCSV(string[] values)
        {


            Id = int.Parse(values[0]);
            Name = values[1];
            LocationId = int.Parse(values[2]);
            Description = values[3];
            LanguageEnum languageEnum;
            if (Enum.TryParse<LanguageEnum>(values[4], out languageEnum))
            {
                Language = languageEnum;
            }
            else
            {
                languageEnum = LanguageEnum.ENGLISH;
                System.Console.WriteLine("Doslo je do greske prilikom ucitavanja jezika");
            }

            MaxGuests = int.Parse(values[5]);
            DurationInHours = int.Parse(values[6]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                LocationId.ToString(),
                Description,
                Language.ToString(),
                MaxGuests.ToString(),
                DurationInHours.ToString(),

            };
            return csvValues;
        }


    }
}
