using BookingProject.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Model.Images;
using System.Xml.Linq;
using BookingProject.Serializer;
using BookingProject.ConversionHelp;

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
        public int GuideId { get; set; }

        public bool IsSuggestion { get; set; }
        public DateTime CreartionDate { get; set; }

        public int ComplexTourRequestId { get; set; }

        public Tour()
        {
            KeyPoints = new List<KeyPoint>();
            StartingTime = new List<TourDateTime>();
            Images = new List<TourImage>();
            IsSuggestion = false;
            CreartionDate = DateTime.Now;
            ComplexTourRequestId = -1;
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
                System.Console.WriteLine("An error occurred while loading the language");
            }

            MaxGuests = int.Parse(values[5]);
            DurationInHours = int.Parse(values[6]);
            GuideId= int.Parse(values[7]);
            IsSuggestion = bool.Parse(values[8]);
            CreartionDate = DateConversion.StringToDateTour(values[9]);
            ComplexTourRequestId = int.Parse(values[10]);
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
                GuideId.ToString(),
                IsSuggestion.ToString(),
                DateConversion.DateToStringTour(CreartionDate),
                ComplexTourRequestId.ToString(),
            };
            return csvValues;
        }
    }
}
