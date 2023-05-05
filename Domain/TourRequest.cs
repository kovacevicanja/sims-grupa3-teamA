using BookingProject.ConversionHelp;
using BookingProject.Domain.Enums;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Domain
{
    public class TourRequest : ISerializable
    {
        public int Id { get; set; }
        public int GuideId { get; set; } 
        public TourRequestStatus Status { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; } 
        public LanguageEnum Language { get; set; }
        public int GuestsNumber { get; set; }   
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public User Guest { get; set; }

        public TourRequest()
        {
            Location = new Location();
            GuideId = -1;
            Guest = new User();
        }
        public TourRequest (int id, int guideId, TourRequestStatus status, Location location, string description, LanguageEnum language, int guestsNumber, DateTime startDate, DateTime endDate, User guest)
        {
            Id = id;
            GuideId = guideId;
            Status = status;
            Location = location;
            Description = description;
            Language = language;
            GuestsNumber = guestsNumber;
            StartDate = startDate;
            EndDate = endDate;
            Guest = guest;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            GuideId = int.Parse(values[1]);
            TourRequestStatus status;
            if (Enum.TryParse<TourRequestStatus>(values[2], out status))
            {
                Status = status;
            }
            else
            {
                Status = TourRequestStatus.PENDING;
                System.Console.WriteLine("An error occurred while loading the status of the tour request!");
            }
            Location.Id = int.Parse(values[3]);
            Description = values[4];
            LanguageEnum language;
            if (Enum.TryParse<LanguageEnum>(values[5], out language))
            {
                Language = language;
            }
            else
            {
                Language = LanguageEnum.ENGLISH;
                System.Console.WriteLine("An error occurred while loading the language of the tour request!");
            }
            GuestsNumber = int.Parse(values[6]);
            StartDate = DateConversion.StringToDateTour(values[7]);
            EndDate = DateConversion.StringToDateTour(values[8]);
            Guest.Id = int.Parse(values[9]);    
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                GuideId.ToString(),
                Status.ToString(),
                Location.Id.ToString(),
                Description,
                Language.ToString(),
                GuestsNumber.ToString(),
                DateConversion.DateToStringTour(StartDate),
                DateConversion.DateToStringTour(EndDate),
                Guest.Id.ToString()
            };
            return csvValues;
        }
    }
}