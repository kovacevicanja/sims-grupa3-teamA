using BookingProject.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Model.Images;
using System.Xml.Linq;

namespace BookingProject.Model
{
    internal class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public LanguageEnum Language { get; set; }
        public int MaxGuests { get; set; }
        public List<string> KeyPoints { get; set; }
        public DateTime StartingTime { get; set; }
        public double DurationInHours { get; set; }
        public List<TourImages> Images { get; set; }

        public Tour() { 
        this.KeyPoints= new List<string>();
        this.Images= new List<TourImages>();
        }

        public Tour(int id, string name, int locationId, string description, LanguageEnum language, int maxGuests, List<string> keyPoints, DateTime startingTime, double durationInHours, List<TourImages> images)
        {
            Id= id;
            Name = name;
            LocationId=locationId;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            KeyPoints = keyPoints;
            StartingTime = startingTime;
            DurationInHours = durationInHours;
            Images = images;
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
                languageEnum = LanguageEnum.English;
                System.Console.WriteLine("Doslo je do greske prilikom ucitavanja jezika");
            }

            MaxGuests = int.Parse(values[5]);
            StartingTime = DateTime.Parse(values[6]);
            DurationInHours = int.Parse(values[7]);
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
                StartingTime.ToString(),
                DurationInHours.ToString(),

            };
            return csvValues;
        }


    }
}
