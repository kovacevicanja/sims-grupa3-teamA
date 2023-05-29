using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using BookingProject.Serializer;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Domain
{
    public class ForumComment : ISerializable
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public Forum Forum { get; set; }
        public bool IsOwners { get; set; }
        public bool IsGuests { get; set; }
        public bool IsInvalid { get; set; }
        public int NumberOfReports { get; set; }
        public ForumComment() {
            User = new User();
            Forum = new Forum();
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Text = values[1];
            User.Id = int.Parse(values[2]);
            Forum.Id = int.Parse(values[3]);
            IsOwners = bool.Parse(values[4]);   
            IsGuests = bool.Parse(values[5]);   
            IsInvalid = bool.Parse(values[6]);
            NumberOfReports= int.Parse(values[7]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Text,
                User.Id.ToString(),
                Forum.Id.ToString(),
                IsOwners.ToString(),
                IsGuests.ToString(),
                IsInvalid.ToString(),
                NumberOfReports.ToString()
            };
            return csvValues;
        }
    }
}
