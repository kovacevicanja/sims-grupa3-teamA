using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Domain
{
    public class SuperGuest : ISerializable 
    {
        public User Guest { get; set; }
        public int NumberOfReservations { get; set; }
        public int BonusPoints { get; set; }
        public DateTime StartDate { get; set; }

        public SuperGuest()
        {
            Guest = new User();
        }

        public void FromCSV(string[] values)
        {
            Guest.Id = int.Parse(values[0]);
            NumberOfReservations = int.Parse(values[1]);
            BonusPoints = int.Parse(values[2]);
            StartDate = DateTime.Parse(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Guest.Id.ToString(),
                NumberOfReservations.ToString(),
                BonusPoints.ToString(),
                StartDate.ToString("dd/MM/yyyy")
        };
            return csvValues;
        }
    }
}
