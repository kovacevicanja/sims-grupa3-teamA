using BookingProject.ConversionHelp;
using BookingProject.Domain.Enums;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Domain
{
    public class ComplexTourRequest : ISerializable
    {
        public int Id { get; set; }  
        public List<TourRequest> TourRequestsList { get; set; }
        public TourRequestStatus Status { get; set; }
        public User Guest { get; set; }

        public ComplexTourRequest() 
        {
            TourRequestsList = new List<TourRequest>(); 
            Guest = new User();
        }

        public ComplexTourRequest(int id, List<TourRequest> tourRequestsList, TourRequestStatus status, User guest )
        {
            Id = id;
            TourRequestsList = tourRequestsList;
            Status = status;
            Guest = guest;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourRequestStatus statusEnum;
            if (Enum.TryParse<TourRequestStatus>(values[1], out statusEnum))
            {
                Status = statusEnum;
            }
            else
            {
                statusEnum = TourRequestStatus.PENDING;
                System.Console.WriteLine("An error occurred while loading the tour request status");
            }
            Guest.Id = int.Parse(values[2]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Status.ToString(),
                Guest.Id.ToString()
            };
            return csvValues;
        }
    }
}